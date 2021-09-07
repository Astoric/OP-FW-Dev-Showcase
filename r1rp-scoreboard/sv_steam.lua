ESX = nil
local mechanic, taxi, fire, cardealer = 0, 0, 0, 0

TriggerEvent('esx:getSharedObject', function(obj) ESX = obj end)

RegisterServerEvent('r1rp-scoreboard:getsteam')
AddEventHandler('r1rp-scoreboard:getsteam', function(srcid)
--AddEventHandler('esx:playerLoaded', function(source)
	local steamid = GetPlayerIdentifier(srcid, 0)
    TriggerClientEvent('r1rp-scoreboard:addplayerid', -1, steamid)
end)

RegisterServerEvent('r1rp-scoreboard:getjobs')
AddEventHandler('r1rp-scoreboard:getjobs', function()
    mechanic, taxi, fire, cardealer = 0, 0, 0, 0
    local xPlayers = ESX.GetPlayers()
	for i=1, #xPlayers, 1 do
        local xPlayer = ESX.GetPlayerFromId(xPlayers[i])
        if xPlayer.job.name == 'mechanic' then
               mechanic = mechanic + 1
        end
        if xPlayer.job.name == 'taxi' then
            taxi = taxi + 1
        end
        if xPlayer.job.name == 'fire' then
            fire = fire + 1
        end
        if xPlayer.job.name == 'cardealer' then
            cardealer = cardealer + 1
        end
    end
    TriggerClientEvent('r1rp-scoreboard:setjobcount', -1, mechanic, taxi, fire, cardealer)
end)