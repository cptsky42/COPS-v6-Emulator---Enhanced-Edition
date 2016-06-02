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

function processTask300012(client, idx)
    name = "TrojanGuard"
    face = 1

    if (idx == 0) then

        if hasItem(client, 710017, 1) then

            text(client, "Since you have passed the test and obtained the Trojan Cert. I will give you the Trojan Amulet.")
            link(client, "Thanks.", 1)
            pic(client, 62)
            create(client)

        else

            text(client, "To obtain the Trojan Amulet, you must enable PK mode to kill the TrojanDevil and give me the Trojan Cert. Are you ready?")
            link(client, "Yeah.", 2)
            link(client, "Not yet.", 255)
            pic(client, 62)
            create(client)

        end

    elseif (idx == 1) then

        spendItem(client, 710017, 1)
        awardItem(client, "710012", 1)

    elseif (idx == 2) then

        if getProfession(client) == 11 then

            move(client, 1052, 71, 172)

        else

            if getProfession(client) == 12 then

                move(client, 1052, 71, 172)

            else

                if getProfession(client) == 13 then

                    move(client, 1052, 71, 172)

                else

                    if getProfession(client) == 14 then

                        move(client, 1052, 71, 172)

                    else

                        if getProfession(client) == 15 then

                            move(client, 1052, 71, 172)

                        else

                            text(client, "Sorry, only Trojan can challenge this test.")
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
