--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:47 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30084(client, idx)
    name = "GeneralQing"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "ArmyToken") and (getMoney(client) >= 0) then

            text(client, "I never expected it was those bandits who robbed the Army Token. We are lucky to have your help. I shall reward you my sword.")
            link(client, "Thanks, sir.", 1)
            pic(client, 7)
            create(client)

        else

         text(client, "I have sent my soldiers out to fetch the monthly provisions with my army token, but I have received no news since they left.")
         link(client, "They must have troubles.", 2)
         link(client, "All will be fine.", 255)
         pic(client, 7)
         create(client)

        end

    elseif (idx == 1) then

        if hasTaskItem(client, "ArmyToken") and (getMoney(client) >= 0) then

            spendItem(client, 721117, 1)
            awardItem(client, "420086", 1)

        end

    elseif (idx == 2) then

        text(client, "That is what I am afraid of. I am worrying about the Army Token. If it is lost, I must be sentenced to death.")
        link(client, "I will try to find it.", 3)
        link(client, "What a bad luck.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 3) then

        text(client, "If you can bring the Army Token back to me, I will give you a handsome reward.")
        link(client, "I shall try my best.", 255)
        pic(client, 7)
        create(client)

    end

end
