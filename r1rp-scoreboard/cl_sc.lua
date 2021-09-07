ESX = nil
local PlayerHasProp, showids = false, true
local PlayerProps = {}
local playersid = {}
local mechanic, taxi, ems, cardealer = 0, 0, 0, 0
local firestatus = ""
local jobsfetched = false

Citizen.CreateThread(function()
    while ESX == nil do
        Citizen.Wait(0)
        TriggerEvent('esx:getSharedObject', function(obj)
            ESX = obj
        end)
    end

    while ESX.GetPlayerData().job == nil do
        Citizen.Wait(10)
    end

    ESX.PlayerData = ESX.GetPlayerData()
end)


local CD = CD or {}

CD.Scoreboard = {}
local forceDraw = false
local shouldDraw = false
local shouldOpenMenu = false

--shouldDraw = true


function CD.Scoreboard.GetPlayerCount(self)
    local count = #GetActivePlayers()
    return count
end

_menuPool = NativeUI.CreatePool()

Citizen.CreateThread(function()
	local currentItemIndex = 1
	local selectedItemIndex = 1

    while true do
        
		Citizen.Wait(0)
	end
end)

Citizen.CreateThread(function()
    while true do
        local ped = GetPlayerPed(-1)
        Citizen.Wait(0)
        _menuPool:ProcessMenus()
        if IsControlJustPressed(1, 303) then
            TriggerEvent('r1rp-scoreboard:getPlayerSteams')
            LoadAnim("missfam4")
            DisableControlAction(0, 172, true)
            DisableControlAction(0, 173, true)
            TaskPlayAnim(GetPlayerPed(-1), "missfam4", "base", 2.0, 2.0, -1, 51, 0, false, false, false)
            AddPropToPlayer('p_amb_clipboard_01', 36029, 0.16, 0.08, 0.1, -130.0, -50.0, 0.0)
            local playerlist = GetActivePlayers()
            mainMenu = NativeUI.CreateMenu("Player List", tostring(CD.Scoreboard:GetPlayerCount()) .. '/32 Online', 1450, 0)
            _menuPool:Add(mainMenu)
            TriggerServerEvent('r1rp-scoreboard:getjobs')
            while not jobsfetched do
                Citizen.Wait(0)
            end
            for i = 1, #playerlist do
                local currPlayer = playerlist[i]
                while playersid[i] == nil do
                    Citizen.Wait(0)
                end
                mainMenu:AddItem(NativeUI.CreateItem("[" .. GetPlayerServerId(currPlayer) .. "] " .. playersid[i], ""))
            end
            _menuPool:RefreshIndex()
            _menuPool:MouseControlsEnabled(false)
            _menuPool:MouseEdgeEnabled(false)
            _menuPool:ControlDisablingEnabled(false)
            mainMenu:Visible(true)
            if(showids) then
                shouldDraw = true
            end
            --TriggerServerEvent('3dme:shareDisplay', "Looking at something")
            Citizen.Wait(0)
        elseif IsControlJustReleased(1, 303) then
            EnableControlAction(0, 172, true)
            EnableControlAction(0, 173, true)
            mainMenu:Visible(false);
            shouldDraw = false
            ClearPedTasks(ped)
            DestroyAllProps()
        end
    end
end)

RegisterCommand("toggleids", function(source, args, rawCommand)
    if(showids) then
        showids = false
        exports['mythic_notify']:DoHudText('error', 'IDs will no longer be shown')
    else
        showids = true
        exports['mythic_notify']:DoHudText('success', 'IDs will be shown')
    end
end, false)

AddEventHandler('playerSpawned', function(spawn)
    return count
end)

function LoadAnim(dict)
    while not HasAnimDictLoaded(dict) do
      RequestAnimDict(dict)
      Wait(10)
    end
  end

function LoadPropDict(model)
    while not HasModelLoaded(GetHashKey(model)) do
      RequestModel(GetHashKey(model))
      Wait(10)
    end
  end

function DestroyAllProps()
    for _,v in pairs(PlayerProps) do
      DeleteEntity(v)
    end
    PlayerHasProp = false
  end

  function AddPropToPlayer(prop1, bone, off1, off2, off3, rot1, rot2, rot3)
    local Player = PlayerPedId()
    local x,y,z = table.unpack(GetEntityCoords(Player))
  
    if not HasModelLoaded(prop1) then
      LoadPropDict(prop1)
    end
  
    prop = CreateObject(GetHashKey(prop1), x, y, z+0.2,  true,  true, true)
    AttachEntityToEntity(prop, Player, GetPedBoneIndex(Player, bone), off1, off2, off3, rot1, rot2, rot3, true, true, false, true, 1, true)
    table.insert(PlayerProps, prop)
    PlayerHasProp = true
    SetModelAsNoLongerNeeded(prop1)
  end

Citizen.CreateThread(function()
    while true do
    Citizen.Wait(0)
    if shouldDraw or forceDraw then
            SetTextFont(4)
            SetTextProportional(1)
            SetTextScale(0.45, 0.45)
            SetTextColour(255, 255, 255, 255)
            SetTextOutline()
            SetTextOutline()
            SetTextEntry("STRING")
            AddTextComponentString("Group Limit: 4\n\nMechanics on duty: "..mechanic .. "\nTaxi on duty: ".. taxi .."\nFire " .. firestatus .. " duty.\nPlenty Police.\nCar Dealers: " .. cardealer)
            DrawText(0.6, 0.8)
    end
end
end)

--Draw Things
Citizen.CreateThread(function()
    local animationState = false
    while true do
        Citizen.Wait(0)

        if shouldDraw or forceDraw then
            local nearbyPlayers = GetNeareastPlayers()
            for k, v in pairs(nearbyPlayers) do
                local x, y, z = table.unpack(v.coords)
                local coordsMe = GetEntityCoords(GetPlayerPed(-1))
                local dist = GetDistanceBetweenCoords(coordsMe['x'], coordsMe['y'], coordsMe['z'], x, y, z, true)
                if dist < 10 then
                    Draw3DText(x, y, z + 1.1, v.playerId)
                end
            end
        end
    end
end)

function Draw3DText(x, y, z, text)
    -- Check if coords are visible and get 2D screen coords
    local onScreen, _x, _y = World3dToScreen2d(x, y, z)
    if onScreen then
        -- Calculate text scale to use
        local dist = GetDistanceBetweenCoords(GetGameplayCamCoords(), x, y, z, 1)
        local scale = 1.8 * (1 / dist) * (1 / GetGameplayCamFov()) * 100

        -- Draw text on screen
        SetTextScale(0.40,0.40)
        SetTextFont(4)
        SetTextOutline()
        SetTextProportional(1)
        SetTextColour(255, 255, 255, 255)
        SetTextDropShadow(0, 0, 0, 0, 255)
        SetTextDropShadow()
        SetTextEdge(4, 0, 0, 0, 255)
        SetTextOutline()
        SetTextEntry("STRING")
        SetTextCentre(1)
        AddTextComponentString(text)
        DrawText(_x, _y)
        local factor = (string.len(text)) / 250
            DrawRect(_x,_y+0.0130, 0.01+ factor, 0.03, 11, 1, 11, 115)
    end
end


function GetNeareastPlayers()
	local playerPed = PlayerPedId()
	local playerlist = GetActivePlayers()
   --local players, _ = ESX.Game.GetPlayersInArea(GetEntityCoords(playerPed), Config.DrawDistance)

    local players_clean = {}
    local found_players = false

    for i = 1, #playerlist, 1 do
        found_players = true
        table.insert(players_clean, { playerName = GetPlayerName(playerlist[i]), playerId = GetPlayerServerId(playerlist[i]), coords = GetEntityCoords(GetPlayerPed(playerlist[i])) })
    end
    return players_clean
end

RegisterNetEvent('r1rp-scoreboard:getPlayerSteams')
AddEventHandler('r1rp-scoreboard:getPlayerSteams', function()
    local playerlist = GetActivePlayers()
            for i = 1, #playerlist do
                local currPlayer = playerlist[i]
                local plysrc = GetPlayerServerId(currPlayer)
                TriggerServerEvent('r1rp-scoreboard:getsteam', plysrc)
            end
end)

RegisterNetEvent('r1rp-scoreboard:addplayerid')
AddEventHandler('r1rp-scoreboard:addplayerid', function(sid)
    table.insert(playersid, sid)
end)

RegisterNetEvent('r1rp-scoreboard:setjobcount')
AddEventHandler('r1rp-scoreboard:setjobcount', function(mechh, taxii, firee, cardealerr)
    mechanic = mechh
    taxi = taxii
    cardealer = cardealerr
    if(firee > 0) then
        firestatus = "on"
    else
        firestatus = "off"
    end
    jobsfetched = true
end)