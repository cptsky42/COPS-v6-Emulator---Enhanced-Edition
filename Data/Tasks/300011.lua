--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:52 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask300011(client, idx)
    name = "WarriorGuard"
    face = 1

    if (idx == 0) then

        if hasItem(client, 710016, 1) then

            text(client, "Since you have passed the test and obtained the Warrior Certificate, I will give you the Warrior Amulet.")
            link(client, "Thanks.", 1)
            pic(client, 62)
            create(client)

        else

            text(client, "To obtain the Warrior Amulet, you must enable PK mode to kill the WarriorDevil and give me the Warrior Cert. Are you ready?")
            link(client, "Yeah.", 2)
            link(client, "Not yet.", 255)
            pic(client, 62)
            create(client)

        end

    elseif (idx == 1) then

        spendItem(client, 710016, 1)
        awardItem(client, "710011", 1)

    elseif (idx == 2) then

        if getProfession(client) == 21 then

            move(client, 1052, 160, 291)

        else

            if getProfession(client) == 22 then

                move(client, 1052, 160, 291)

            else

                if getProfession(client) == 23 then

                    move(client, 1052, 160, 291)

                else

                    if getProfession(client) == 24 then

                        move(client, 1052, 160, 291)

                    else

                        if getProfession(client) == 25 then

                            move(client, 1052, 160, 291)

                        else

                            text(client, "Sorry, only Warrior can challenge this test.")
                            link(client, "I see.", 255)
                            pic(client, 62)
                            create(client)

                        end

                    end

                end

            end

        end

    end

end
