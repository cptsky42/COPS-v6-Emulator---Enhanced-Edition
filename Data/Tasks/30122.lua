--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:50 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30122(client, idx)
    name = "Tientsin"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721156, 1) then

            text(client, "JailGuard is nearby. Try your luck to find it out.")
            link(client, "Thanks. I will.", 255)
            pic(client, 61)
            create(client)

        else

            if hasItem(client, 721155, 1) then

                text(client, "You did pass the 2nd floor. I will keep my words. Take the passport and find Jail Guard to get into Violet Jail.")
                link(client, "Thanks.", 1)
                pic(client, 61)
                create(client)

            else

                if hasItem(client, 721154, 1) then

                    text(client, "You haven`t passed the second floor of Skypass. So you can`t enter the Violet Jail.")
                    link(client, "I will get the passport soon.", 255)
                    pic(client, 61)
                    create(client)

                else

                    if hasItem(client, 721153, 1) then

                        text(client, "Hey. Are you sent by DevineArtisan and Hades to get WaterRing? They have tried many times in vain.")
                        link(client, "I might have a try as well.", 2)
                        link(client, "Alright, I give up.", 255)
                        pic(client, 61)
                        create(client)

                    else

                        text(client, "What brings you here?")
                        link(client, "Just pass by.", 255)
                        pic(client, 61)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 1) then

        spendItem(client, 721155, 1)
        awardItem(client, "721156", 1)

    elseif (idx == 2) then

        text(client, "Good. If you want to get the Passport from me, pass the sceond floor of SkyPass first!")
        link(client, "No problem.", 3)
        link(client, "Ok. Wait a moment.", 255)
        pic(client, 61)
        create(client)

    elseif (idx == 3) then

        awardItem(client, "721154", 1)

    end

end
