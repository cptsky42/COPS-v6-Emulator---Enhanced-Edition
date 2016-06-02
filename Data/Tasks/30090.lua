--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:48 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30090(client, idx)
    name = "GuardingKid"
    face = 1

    if (idx == 0) then

        text(client, "You have great to make it so far. Only one of us will send you to claim the prize. Please believe me.")
        link(client, "Alright. I believe you.", 1)
        link(client, "Let me think it over.", 255)
        pic(client, 10)
        create(client)

    elseif (idx == 1) then

        if (rand(client, 2) < 1) then

            moveNpc(client, 30090, 1041, 510, 324)
            action = randomAction(client, 1, 8)
            if action == 1 then
                move(client, 1041, 323, 334)
                text(client, "Aha, you are cheated. I never teleport you to the prize maze. You are teleported back to another maze. Please try again. Good l")
                link(client, "What a bad luck.", 255)
                pic(client, 10)
                create(client)
            elseif action == 2 then
                move(client, 1041, 343, 361)
                text(client, "Aha, you are cheated. I never teleport you to the prize maze. You are teleported back to another maze. Please try again. Good l")
                link(client, "What a bad luck.", 255)
                pic(client, 10)
                create(client)
            elseif action == 3 then
                move(client, 1041, 365, 307)
                text(client, "Aha, you are cheated. I never teleport you to the prize maze. You are teleported back to another maze. Please try again. Good l")
                link(client, "What a bad luck.", 255)
                pic(client, 10)
                create(client)
            elseif action == 4 then
                move(client, 1041, 387, 364)
                text(client, "Aha, you are cheated. I never teleport you to the prize maze. You are teleported back to another maze. Please try again. Good l")
                link(client, "What a bad luck.", 255)
                pic(client, 10)
                create(client)
            elseif action == 5 then
                move(client, 1041, 399, 337)
                text(client, "Aha, you are cheated. I never teleport you to the prize maze. You are teleported back to another maze. Please try again. Good l")
                link(client, "What a bad luck.", 255)
                pic(client, 10)
                create(client)
            elseif action == 6 then
                move(client, 1041, 405, 324)
                text(client, "Aha, you are cheated. I never teleport you to the prize maze. You are teleported back to another maze. Please try again. Good l")
                link(client, "What a bad luck.", 255)
                pic(client, 10)
                create(client)
            elseif action == 7 then
                move(client, 1041, 382, 273)
                text(client, "Aha, you are cheated. I never teleport you to the prize maze. You are teleported back to another maze. Please try again. Good l")
                link(client, "What a bad luck.", 255)
                pic(client, 10)
                create(client)
            elseif action == 8 then
                move(client, 1041, 357, 337)
                text(client, "Aha, you are cheated. I never teleport you to the prize maze. You are teleported back to another maze. Please try again. Good l")
                link(client, "What a bad luck.", 255)
                pic(client, 10)
                create(client)
            end


        else

            action = randomAction(client, 1, 8)
            if action == 1 then
                move(client, 1041, 323, 334)
                setRecordPos(client, 1020, 566, 565)
                text(client, "The monsters in the prize claiming maze are very ferocious. Be careful.")
                link(client, "Thanks.", 255)
                pic(client, 10)
                create(client)
            elseif action == 2 then
                move(client, 1041, 343, 361)
                setRecordPos(client, 1020, 566, 565)
                text(client, "The monsters in the prize claiming maze are very ferocious. Be careful.")
                link(client, "Thanks.", 255)
                pic(client, 10)
                create(client)
            elseif action == 3 then
                move(client, 1041, 365, 307)
                setRecordPos(client, 1020, 566, 565)
                text(client, "The monsters in the prize claiming maze are very ferocious. Be careful.")
                link(client, "Thanks.", 255)
                pic(client, 10)
                create(client)
            elseif action == 4 then
                move(client, 1041, 387, 364)
                setRecordPos(client, 1020, 566, 565)
                text(client, "The monsters in the prize claiming maze are very ferocious. Be careful.")
                link(client, "Thanks.", 255)
                pic(client, 10)
                create(client)
            elseif action == 5 then
                move(client, 1041, 399, 337)
                setRecordPos(client, 1020, 566, 565)
                text(client, "The monsters in the prize claiming maze are very ferocious. Be careful.")
                link(client, "Thanks.", 255)
                pic(client, 10)
                create(client)
            elseif action == 6 then
                move(client, 1041, 405, 324)
                setRecordPos(client, 1020, 566, 565)
                text(client, "The monsters in the prize claiming maze are very ferocious. Be careful.")
                link(client, "Thanks.", 255)
                pic(client, 10)
                create(client)
            elseif action == 7 then
                move(client, 1041, 382, 273)
                setRecordPos(client, 1020, 566, 565)
                text(client, "The monsters in the prize claiming maze are very ferocious. Be careful.")
                link(client, "Thanks.", 255)
                pic(client, 10)
                create(client)
            elseif action == 8 then
                move(client, 1041, 357, 337)
                setRecordPos(client, 1020, 566, 565)
                text(client, "The monsters in the prize claiming maze are very ferocious. Be careful.")
                link(client, "Thanks.", 255)
                pic(client, 10)
                create(client)
            end


        end

    end

end
