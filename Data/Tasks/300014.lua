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

function processTask300014(client, idx)
    name = "WaterGuard"
    face = 1

    if (idx == 0) then

        if hasItem(client, 710019, 1) then

            text(client, "Since you have passed the test and obtained the Water Certificate, I will give you the Water Amulet.")
            link(client, "Thanks.", 1)
            pic(client, 62)
            create(client)

        else

            text(client, "To obtain the WaterAmulet, you must enable PK mode to kill the WaterDevil and give me the Water Cert. Are you ready?")
            link(client, "Yeah.", 2)
            link(client, "Not yet.", 255)
            pic(client, 62)
            create(client)

        end

    elseif (idx == 1) then

        spendItem(client, 710019, 1)
        awardItem(client, "710014", 1)

    elseif (idx == 2) then

        if getProfession(client) == 101 then

            move(client, 1052, 295, 163)

        else

            if getProfession(client) == 132 then

                move(client, 1052, 295, 163)

            else

                if getProfession(client) == 133 then

                    move(client, 1052, 295, 163)

                else

                    if getProfession(client) == 134 then

                        move(client, 1052, 295, 163)

                    else

                        if getProfession(client) == 135 then

                            move(client, 1052, 295, 163)

                        else

                            text(client, "Sorry, only WaterTaoist can challenge this test.")
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
