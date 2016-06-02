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

function processTask30112(client, idx)
    name = "GeneralChen"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721133, 1) then

            text(client, "Well done! Since you are new to the world, I will send you a sword as a reward.")
            link(client, "Thank you.", 1)
            pic(client, 37)
            create(client)

        else

            if hasItem(client, 721134, 1) then

                text(client, "He likes to travel around. If you cannot find him, you may ask his soldier for more information.")
                link(client, "I see.", 255)
                pic(client, 37)
                create(client)

            else

                if getLevel(client) < 41 then

                    text(client, "Hello! I have a quest for you to take. I am sure that you can finish it. Are you interested?")
                    link(client, "Tell me more.", 2)
                    link(client, "No, thanks.", 255)
                    pic(client, 37)
                    create(client)

                else

                    text(client, "Quarrels and disputes come up from time to time. It seems that troubled times return.")
                    link(client, "That is what I am coming for.", 255)
                    pic(client, 37)
                    create(client)

                end

            end

        end

    elseif (idx == 1) then

        spendItem(client, 721133, 1)
        awardItem(client, "410057", 1)

    elseif (idx == 2) then

        text(client, "Here is an urgent mail for General Wu in Phoenix Castle. Can you help me to take this mail to him?")
        link(client, "Sure.", 3)
        link(client, "Sorry, I am too busy.", 255)
        pic(client, 37)
        create(client)

    elseif (idx == 3) then

        awardItem(client, "721134", 1)
        text(client, "Please bring me his letter in reply after you meet him. I shall give you a good reward.")
        link(client, "I see.", 255)
        pic(client, 37)
        create(client)

    end

end
