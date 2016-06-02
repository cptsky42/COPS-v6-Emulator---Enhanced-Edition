--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/21/2015 1:36:21 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function useItem1060101(self, client)
    name = "FireofHell"
    face = 1

    if getLevel(client) < 82 then

        text(client, "Sorry, only Fire Wizards can learn this spell after they reach level 82.")
        link(client, "I see.", 255)
        create(client)

    else

        if getProfession(client) == 143 then

            awardMagic(client, 1165, 0)
            text(client, "You have learned Fire of Hell.")
            link(client, "Thanks.", 255)
            create(client)

        else

            if getProfession(client) == 144 then

                awardMagic(client, 1165, 0)
                text(client, "You have learned Fire of Hell.")
                link(client, "Thanks.", 255)
                create(client)

            else

                if getProfession(client) == 145 then

                    awardMagic(client, 1165, 0)
                    text(client, "You have learned Fire of Hell.")
                    link(client, "Thanks.", 255)
                    create(client)

                else

                    text(client, "Sorry, only Fire Wizards can learn this spell after they reach level 82.")
                    link(client, "I see.", 255)
                    create(client)

                end

            end

        end

    end

end
