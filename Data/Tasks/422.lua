--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 4/1/2015 7:41:27 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask422(client, idx)
    name = "OldQuarrier"
    face = 1

    if (idx == 0) then

        if hasItem(client, 1072031, 10) then

            text(client, "Since you take 10 EuxeniteOres, I`ll refine Saltpeter for you.")
            link(client, "Thank you.", 1)
            link(client, "Oh, wait.", 255)
            pic(client, 91)
            create(client)

        else

            if hasItem(client, 1072031, 1) then

                text(client, "Ten EuxeniteOres are required to make a piece of Saltpeter. Please get ores ready.")
                link(client, "What is Saltpeter used for?", 2)
                link(client, "OK.", 255)
                pic(client, 91)
                create(client)

            else

                text(client, "Norbert recommended me to you, did he?")
                link(client, "Yes. I come for Saltpeter.", 3)
                link(client, "No. Just passing by.", 2)
                pic(client, 91)
                create(client)

            end

        end

    elseif (idx == 1) then

        if hasItem(client, 1072031, 10) then

            spendItem(client, 1072031, 10)
            awardItem(client, "721262", 1)
            text(client, "My pleasure. Here you are.")
            link(client, "Thank you.", 255)
            pic(client, 91)
            create(client)

        else

            text(client, "I`m busy. Please don`t bother me if you don`t have 10 EuxeniteOres.")
            link(client, "I`m very sorry.", 255)
            pic(client, 91)
            create(client)

        end

    elseif (idx == 2) then

        text(client, "Are you coming for Saltpeter? Since Norbert invented Bomb, I have been kept busy with supplying Saltpeter for him.")
        link(client, "What is the Bomb?", 4)
        link(client, "Sorry to bother you.", 255)
        pic(client, 91)
        create(client)

    elseif (idx == 3) then

        text(client, "Let`s make long story short. 10 EuxeniteOres are required for a piece of Saltpeter. You can dig EuxeniteOres in the plain mine.")
        link(client, "I will get some.", 255)
        pic(client, 91)
        create(client)

    elseif (idx == 4) then

        text(client, "It is powerful and used to bomb out City Gate. If you`re interested in it, you can visit Norbert in the small village of the fo")
        link(client, "I see.", 255)
        pic(client, 91)
        create(client)

    end

end
