--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:44 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask20120(client, idx)
    name = "ArenaGuard"
    face = 1

    if (idx == 0) then

        if checkTime(client, 4, "21:27 21:30") then

            text(client, "Are you heading for the next PK map?")
            link(client, "Yes.", 1)
            link(client, "Wait a minute.", 255)
            pic(client, 7)
            create(client)

        else

            checkTime(client, 4, "22:27 22:30")
            text(client, "Are you heading for the next PK map?")
            link(client, "Yes.", 2)
            link(client, "Wait a minute.", 255)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 1) then

        action = randomAction(client, 1, 8)
        if action == 1 then
            move(client, 1094, 101, 75)
        elseif action == 2 then
            move(client, 1094, 80, 63)
        elseif action == 3 then
            move(client, 1094, 54, 53)
        elseif action == 4 then
            move(client, 1094, 101, 75)
        elseif action == 5 then
            move(client, 1094, 104, 113)
        elseif action == 6 then
            move(client, 1094, 80, 63)
        elseif action == 7 then
            move(client, 1094, 54, 53)
        elseif action == 8 then
            move(client, 1094, 104, 113)
        end


    elseif (idx == 2) then

        action = randomAction(client, 1, 8)
        if action == 1 then
            move(client, 1095, 31, 30)
        elseif action == 2 then
            move(client, 1095, 31, 30)
        elseif action == 3 then
            move(client, 1095, 32, 23)
        elseif action == 4 then
            move(client, 1095, 32, 23)
        elseif action == 5 then
            move(client, 1095, 31, 30)
        elseif action == 6 then
            move(client, 1095, 31, 30)
        elseif action == 7 then
            move(client, 1095, 32, 23)
        elseif action == 8 then
            move(client, 1095, 32, 23)
        end


    end

end
