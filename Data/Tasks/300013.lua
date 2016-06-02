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

function processTask300013(client, idx)
    name = "FireGuard"
    face = 1

    if (idx == 0) then

        if hasItem(client, 710018, 1) then

            text(client, "Since you have passed the test and obtained the Fire Certificate, I will give you the Fire Amulet.")
            link(client, "Thanks.", 1)
            pic(client, 62)
            create(client)

        else

            text(client, "To obtain the Fire Amulet, you must enable PK mode to kill the FireDevil and give me the Fire Cert. Are you ready for the test?")
            link(client, "Yeah.", 2)
            link(client, "Not yet.", 255)
            pic(client, 62)
            create(client)

        end

    elseif (idx == 1) then

        spendItem(client, 710018, 1)
        awardItem(client, "710013", 1)

    elseif (idx == 2) then

        if getProfession(client) == 101 then

            move(client, 1052, 160, 63)

        else

            if getProfession(client) == 142 then

                move(client, 1052, 160, 63)

            else

                if getProfession(client) == 143 then

                    move(client, 1052, 160, 63)

                else

                    if getProfession(client) == 144 then

                        move(client, 1052, 160, 63)

                    else

                        if getProfession(client) == 145 then

                            move(client, 1052, 160, 63)

                        else

                            text(client, "Sorry, only FireTaoist can challenge this test.")
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
