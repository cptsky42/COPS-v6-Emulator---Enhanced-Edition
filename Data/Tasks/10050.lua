--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:42 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask10050(client, idx)
    name = "Conductress"
    face = 1

    if (idx == 0) then

        text(client, "Where are you heading for? I can teleport you for a price of 100 silver.")
        link(client, "Phoenix Castle", 1)
        link(client, "Desert City", 2)
        link(client, "Ape Mountain", 3)
        link(client, "Bird Island.", 4)
        link(client, "Mine Cave", 5)
        link(client, "Market", 6)
        link(client, "Just passing by.", 255)
        pic(client, 1)
        create(client)

    elseif (idx == 1) then

        if spendMoney(client, 100) then

            move(client, 1002, 958, 555)

        else

            text(client, "Sorry, you do not have 100 silver.")
            link(client, "I see.", 255)
            pic(client, 1)
            create(client)

        end

    elseif (idx == 2) then

        if spendMoney(client, 100) then

            move(client, 1002, 69, 473)

        else

            text(client, "Sorry, you do not have 100 silver.")
            link(client, "I see.", 255)
            pic(client, 1)
            create(client)

        end

    elseif (idx == 3) then

        if spendMoney(client, 100) then

            move(client, 1002, 555, 957)

        else

            text(client, "Sorry, you do not have 100 silver.")
            link(client, "I see.", 255)
            pic(client, 1)
            create(client)

        end

    elseif (idx == 4) then

        if spendMoney(client, 100) then

            move(client, 1002, 232, 190)

        else

            text(client, "Sorry, you do not have 100 silver.")
            link(client, "I see.", 255)
            pic(client, 1)
            create(client)

        end

    elseif (idx == 5) then

        if spendMoney(client, 100) then

            move(client, 1002, 53, 399)

        else

            text(client, "Sorry, you do not have 100 silver.")
            link(client, "I see.", 255)
            pic(client, 1)
            create(client)

        end

    elseif (idx == 6) then

        if isWanted(client) then

            text(client, "You are the wanted criminal. It would be dangerous for me to teleport you to the market, so I shall charge you 1000 silver.")
            link(client, "OK. Here you are.", 7)
            link(client, "I changed my mind.", 255)
            pic(client, 10)
            create(client)

        else

            if spendMoney(client, 100) then

                setRecordPos(client, 1002, 430, 380)
                move(client, 1036, 291, 236)

            else

                text(client, "Sorry, you do not have 100 silver.")
                link(client, "I see.", 255)
                pic(client, 1)
                create(client)

            end

        end

    elseif (idx == 7) then

        if spendMoney(client, 1000) then

            setRecordPos(client, 1002, 430, 380)
            move(client, 1036, 291, 236)

        else

            text(client, "Sorry, you do not have 1,000 silver.")
            link(client, "I see.", 255)
            pic(client, 10)
            create(client)

        end

    end

end
