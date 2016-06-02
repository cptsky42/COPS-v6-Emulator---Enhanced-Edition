--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:47 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30082(client, idx)
    name = "GeneralCai"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "NameCard") and (getMoney(client) >= 0) then

            if getLevel(client) < 16 then

                text(client, "Sorry, your level is too low. I may help you when you are level 16.")
                link(client, "I shall come later.", 255)
                pic(client, 9)
                create(client)

            else

                if getLevel(client) < 31 then

                    text(client, "Since Coach Li introduced you to me, I shall try to help you.")
                    link(client, "Thanks, sir.", 1)
                    pic(client, 9)
                    create(client)

                else

                    text(client, "You should have played Conquer for a while. I believe you would exceed me soon.")
                    link(client, "I hope so.", 255)
                    pic(client, 9)
                    create(client)

                end

            end

        else

         text(client, "I have been in circle for many years and gained rich experience. I do not give my experience to others easily.")
         link(client, "You are too arrogant.", 255)
         pic(client, 9)
         create(client)

        end

    elseif (idx == 1) then

        if hasTaskItem(client, "NameCard") and (getMoney(client) >= 0) then

            text(client, "Please give me a Euxenite Ore before I reward you some experience.")
            link(client, "Here you are.", 2)
            pic(client, 9)
            create(client)

        end

    elseif (idx == 2) then

        if hasTaskItem(client, "NameCard") and (getMoney(client) >= 0) then

            if hasItem(client, 1072031, 1) then

                text(client, "Since you are sincere, I shall give you 8,000 experience points.")
                link(client, "Thanks.", 3)
                pic(client, 9)
                create(client)

            else

                text(client, "Sorry, you do not have a Euxenite Ore.")
                link(client, "I shall give you later.", 255)
                pic(client, 9)
                create(client)

            end

        end

    elseif (idx == 3) then

        if hasTaskItem(client, "NameCard") and (getMoney(client) >= 0) then

            spendItem(client, 1072031, 1)
            spendItem(client, 721116, 1)
            addExp(client, 8000)

        end

    end

end
