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

function processTask30103(client, idx)
    name = "StoneBandit"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721129, 1) then

            text(client, "Anything else? Rock Bandits are faithful too though sometimes... We have given you the promise.")
            link(client, "Well done.", 255)
            pic(client, 22)
            create(client)

        else

            if hasItem(client, 721126, 1) then

                if hasItem(client, 721128, 1) then

                    text(client, "We acknowlege the corn. This is our promise. We won`t charge the water fee from the villager.")
                    link(client, "Haha...", 1)
                    pic(client, 22)
                    create(client)

                else

                    if hasItem(client, 721126, 1) then

                        text(client, "It`s none of your business. You`d better go out of my sight.")
                        link(client, "I don`t fear you at all.", 2)
                        pic(client, 22)
                        create(client)

                    else

                        text(client, "Go out from my sight or I will kill you.")
                        link(client, "I am busy.", 255)
                        pic(client, 22)
                        create(client)

                    end

                end

            else

                text(client, "Go out from my sight or I will kill you.")
                link(client, "I am busy.", 255)
                pic(client, 22)
                create(client)

            end

        end

    elseif (idx == 1) then

        spendItem(client, 721126, 1)
        spendItem(client, 721128, 1)
        awardItem(client, "721129", 1)

    elseif (idx == 2) then

        text(client, "If you can defeat the StoneBandits and get BrokenSword, I will stop my behavior.")
        link(client, "Don`t regret for what you said.", 255)
        pic(client, 22)
        create(client)

    end

end
