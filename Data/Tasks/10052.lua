--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:43 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask10052(client, idx)
    name = "Conductress"
    face = 1

    if (idx == 0) then

        text(client, "Where are you heading for? I can teleport you for a price of 100 silver.")
        link(client, "Twin City", 1)
        link(client, "Market", 2)
        link(client, "Just passing by.", 255)
        pic(client, 1)
        create(client)

    elseif (idx == 1) then

        if spendMoney(client, 100) then

            move(client, 1011, 11, 376)

        else

            text(client, "Sorry, you do not have 100 silver.")
            link(client, "I see.", 255)
            pic(client, 1)
            create(client)

        end

    elseif (idx == 2) then

        if isWanted(client) then

            text(client, "You are the wanted criminal. It would be dangerous for me to teleport you to the market, so I shall charge you 1000 silver.")
            link(client, "OK. Here you are.", 3)
            link(client, "I changed my mind.", 255)
            pic(client, 10)
            create(client)

        else

            if spendMoney(client, 100) then

                setRecordPos(client, 1011, 193, 266)
                move(client, 1036, 291, 236)

            else

                text(client, "Sorry, you do not have 100 silver.")
                link(client, "I see.", 255)
                pic(client, 1)
                create(client)

            end

        end

    elseif (idx == 3) then

        if spendMoney(client, 1000) then

            setRecordPos(client, 1011, 193, 266)
            move(client, 1036, 291, 236)

        else

            text(client, "Sorry, you do not have 1,000 silver.")
            link(client, "I see.", 255)
            pic(client, 10)
            create(client)

        end

    end

end
