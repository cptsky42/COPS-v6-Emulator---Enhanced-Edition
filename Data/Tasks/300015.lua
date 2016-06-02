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

function processTask300015(client, idx)
    name = "ArcherGuard"
    face = 1

    if (idx == 0) then

        if hasItem(client, 710020, 1) then

            text(client, "Since you have passed the test and obtained the Archer Cert, I will give you the Archer Amulet.")
            link(client, "Thanks.", 1)
            pic(client, 62)
            create(client)

        else

            text(client, "To obtain the Archer Amulet, you must enable PK mode to kill the ArcherDevil and give me the Archer Cert. Are you ready?")
            link(client, "Yeah.", 2)
            link(client, "Not yet.", 255)
            pic(client, 62)
            create(client)

        end

    elseif (idx == 1) then

        spendItem(client, 710020, 1)
        awardItem(client, "710015", 1)

    elseif (idx == 2) then

        if getProfession(client) == 41 then

            move(client, 1052, 316, 313)

        else

            if getProfession(client) == 42 then

                move(client, 1052, 316, 313)

            else

                if getProfession(client) == 43 then

                    move(client, 1052, 316, 313)

                else

                    if getProfession(client) == 44 then

                        move(client, 1052, 316, 313)

                    else

                        if getProfession(client) == 45 then

                            move(client, 1052, 316, 313)

                        else

                            text(client, "Sorry, only Archer can challenge this test.")
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
