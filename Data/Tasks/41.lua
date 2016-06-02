--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 4/20/2015 7:53:34 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask41(client, idx)
    name = "ArtisanOu"
    face = 1

    if (idx == 0) then

        text(client, "It is very difficult to own a gemmy weapon. I am famous for my excellent skills. What can I do for you?")
        link(client, "Make a socket in my weapon.", 1)
        link(client, "Just passing by.", 255)
        pic(client, 10)
        create(client)

    elseif (idx == 1) then

        text(client, "If you give me a Dragon Ball, I shall try my best to help you.")
        link(client, "OK. Here is a Dragon Ball.", 2)
        link(client, "I changed my mind.", 255)
        pic(client, 10)
        create(client)

    elseif (idx == 2) then

        text(client, "Please equip the right handed weapon you want to socket first. Now I am going to make a socket in your weapon.")
        link(client, "I am ready.", 3)
        link(client, "I changed my mind.", 255)
        pic(client, 10)
        create(client)

    elseif (idx == 3) then

        if hasTaskItem(client, "DragonBall") and (getMoney(client) >= 0) then

            if chkEquipHole(client, 1) then

                text(client, "Wow, your weapon has a socket. It is so rare. Are you here for another socket. If yes, 5 Dragon Balls will be charged.")
                link(client, "Here are 5 Dragon Balls.", 4)
                link(client, "I changed my mind.", 255)
                pic(client, 10)
                create(client)

            else

                makeEquipHole(client, 1)

                spendItem(client, 1088000, 1)
                text(client, "Your weapon has been socketed. Please kindly check.")
                link(client, "Thanks.", 255)
                pic(client, 10)
                create(client)

            end

        end

    elseif (idx == 4) then

        if hasItem(client, 1088000, 5) then

            makeEquipHole(client, 2)

            spendItem(client, 1088000, 5)
            text(client, "Your weapon has been socketed. Please kindly check.")
            link(client, "Thanks.", 255)
            pic(client, 10)
            create(client)

        else

            text(client, "Sorry, you do not have the required Dragon Ball(s).")
            link(client, "I see.", 255)
            pic(client, 10)
            create(client)

        end

    end

end
