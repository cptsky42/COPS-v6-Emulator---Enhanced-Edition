--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 4/20/2015 8:07:03 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask380(client, idx)
    name = "GuildController"
    face = 1

    if (idx == 0) then

        text(client, "What can I do for you?")
        link(client, "Enter the guild area.", 1)
        link(client, "Buy statue.", 2)
        link(client, "Just passing by.", 255)
        pic(client, 123)
        create(client)

    elseif (idx == 1) then

        if checkTime(client, 3, "5 14:00 5 23:59") then

            action = randomAction(client, 1, 8)
            if action == 1 then
                move(client, 1038, 351, 341)
            elseif action == 2 then
                move(client, 1038, 336, 279)
            elseif action == 3 then
                move(client, 1038, 319, 282)
            elseif action == 4 then
                move(client, 1038, 316, 324)
            elseif action == 5 then
                move(client, 1038, 320, 357)
            elseif action == 6 then
                move(client, 1038, 321, 372)
            elseif action == 7 then
                move(client, 1038, 301, 373)
            elseif action == 8 then
                move(client, 1038, 334, 341)
            end


        else

            if checkTime(client, 3, "6 00:00 6 19:59") then

                action = randomAction(client, 1, 8)
                if action == 1 then
                    move(client, 1038, 351, 341)
                elseif action == 2 then
                    move(client, 1038, 336, 279)
                elseif action == 3 then
                    move(client, 1038, 319, 282)
                elseif action == 4 then
                    move(client, 1038, 316, 324)
                elseif action == 5 then
                    move(client, 1038, 320, 357)
                elseif action == 6 then
                    move(client, 1038, 321, 372)
                elseif action == 7 then
                    move(client, 1038, 301, 373)
                elseif action == 8 then
                    move(client, 1038, 334, 341)
                end


            else

                move(client, 1038, 351, 341)

            end

        end

    elseif (idx == 2) then

        text(client, "A statue costs 50,000 silver. Are you sure that you want to buy?")
        link(client, "Yeah.", 3)
        link(client, "No, thanks.", 255)
        pic(client, 123)
        create(client)

    elseif (idx == 3) then

        if getMoney(client) < 50000 then

            text(client, "Sorry, you do not have 50000 silver.")
            link(client, "I see.", 255)
            pic(client, 123)
            create(client)

        else

            awardItem(client, "720020", 1)
            spendMoney(client, 50000)

        end

    end

end
