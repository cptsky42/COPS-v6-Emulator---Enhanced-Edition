--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:56 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask600066(client, idx)
    name = "ToughWei"
    face = 1

    if (idx == 0) then

        text(client, "Bug off at once if you have nothing to do here!")
        link(client, "It is terrible.", 255)
        link(client, "I come here for help.", 1)
        pic(client, 8)
        create(client)

    elseif (idx == 1) then

        text(client, "Hum, what? Tell me right away. Do not waste my time. I am busy.")
        link(client, "I have a MoonLetter.", 2)
        link(client, "I will go.", 255)
        pic(client, 8)
        create(client)

    elseif (idx == 2) then

        if hasItem(client, 723005, 1) then

            text(client, "Ha-ha... It is wonderful. I have been looking for it so long.")
            text(client, "I will give you 40,000 silver. Now I can report it to my boss. Ha-ha!")
            link(client, "Here you are.", 3)
            link(client, "I leave it to myself.", 255)
            pic(client, 8)
            create(client)

        else

            if hasItem(client, 723006, 1) then

                text(client, "Ha-ha... It is wonderful. I have been looking for it so long.")
                text(client, "I will give you 40,000 silver. Now I can report it to my boss. Ha-ha!")
                link(client, "Here you are.", 3)
                link(client, "I leave it to myself.", 255)
                pic(client, 8)
                create(client)

            else

                text(client, "Get out of here right away.")
                link(client, "Well.", 255)
                pic(client, 8)
                create(client)

            end

        end

    elseif (idx == 3) then

        if spendItem(client, 723005, 1) then

            gainMoney(client, 40000)

        else

            spendItem(client, 723006, 1)
            gainMoney(client, 40000)

        end

    end

end
