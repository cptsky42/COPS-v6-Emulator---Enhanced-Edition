--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:49 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30105(client, idx)
    name = "HeresyLeader"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721130, 1) then

            text(client, "With the written promise, we shall not harry human being any more.")
            link(client, "Well.", 255)
            pic(client, 10)
            create(client)

        else

            if hasItem(client, 721124, 1) then

                if hasItem(client, 721123, 1) then

                    text(client, "I have thought you were unable to overcome us. I promise you that we will stick to this area in the future.")
                    link(client, "I want a written promise.", 1)
                    pic(client, 10)
                    create(client)

                else

                    text(client, "You do have some stuff. Even my tough men have given in.")
                    link(client, "Just a piece of cake.", 255)
                    pic(client, 10)
                    create(client)

                end

            else

                if hasItem(client, 721123, 1) then

                    text(client, "Human being are coward. They are frightened to death and send out a novice like you. We shall eat you up sooner or later.")
                    link(client, "You are arrogant.", 2)
                    pic(client, 10)
                    create(client)

                else

                    text(client, "Get out of my sight, or I shall eat you.")
                    link(client, "Well.", 255)
                    pic(client, 10)
                    create(client)

                end

            end

        end

    elseif (idx == 1) then

        text(client, "That is alright. We always keep our promise.")
        link(client, "Give it to me.", 3)
        pic(client, 10)
        create(client)

    elseif (idx == 2) then

        text(client, "In my clan, everyone is cruel and tough. It is no way that human can defeat us, let alone a single effort from you.")
        link(client, "Let us wait and see.", 4)
        pic(client, 10)
        create(client)

    elseif (idx == 3) then

        spendItem(client, 721123, 1)
        spendItem(client, 721124, 1)
        awardItem(client, "721130", 1)

    elseif (idx == 4) then

        text(client, "OK. If you can defeat my men, I promise you not to harry human being.")
        link(client, "Do not break your promise.", 255)
        pic(client, 10)
        create(client)

    end

end
