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

function processTask600003(client, idx)
    name = "Maggie"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "SoulJade") and (getMoney(client) >= 0) then

            text(client, "With your Soul Jade, you can enter the Life Tactic. Would you like to enter this tactic now?")
            link(client, "Yeah.", 1)
            link(client, "No, thanks.", 255)
            pic(client, 9)
            create(client)

        else

         text(client, "What are you here for? Please do not go ahead, or you will enter a very dangerous tactic.")
         link(client, "Such tactics do exist?", 2)
         link(client, "I decide to try.", 3)
         link(client, "I had better leave here.", 255)
         pic(client, 9)
         create(client)

        end

    elseif (idx == 1) then

        move(client, 1050, 211, 164)

    elseif (idx == 2) then

        text(client, "The tactics are most changeful. I once thought highly of myself and died from the damned tactics.")
        link(client, "How can I break through?", 4)
        link(client, "I do not believe it.", 255)
        pic(client, 9)
        create(client)

    elseif (idx == 3) then

        text(client, "Since you like challenge, I shall teleport you there. Be careful and remember to pick up the token dropped by the monsters.")
        link(client, "Okay.", 5)
        pic(client, 9)
        create(client)

    elseif (idx == 4) then

        text(client, "I have studied it for many years, but fail to work it out. It is similar to the said Eight-Diagram Tactics. Do you want to try?")
        link(client, "Yeah. I like challenge.", 6)
        link(client, "No. I changed my mind.", 255)
        pic(client, 9)
        create(client)

    elseif (idx == 5) then

        action = randomAction(client, 1, 8)
        if action == 1 then
            move(client, 1043, 211, 164)
        elseif action == 2 then
            move(client, 1044, 211, 164)
        elseif action == 3 then
            move(client, 1045, 211, 164)
        elseif action == 4 then
            move(client, 1046, 211, 164)
        elseif action == 5 then
            move(client, 1047, 211, 164)
        elseif action == 6 then
            move(client, 1048, 211, 164)
        elseif action == 7 then
            move(client, 1049, 211, 164)
        elseif action == 8 then
            action = randomAction(client, 1, 7)
            if action == 1 then
                move(client, 1043, 211, 164)
            elseif action == 2 then
                move(client, 1044, 211, 164)
            elseif action == 3 then
                move(client, 1045, 211, 164)
            elseif action == 4 then
                move(client, 1046, 211, 164)
            elseif action == 5 then
                move(client, 1047, 211, 164)
            elseif action == 6 then
                move(client, 1048, 211, 164)
            elseif action == 7 then
                move(client, 1049, 211, 164)
            end

        end


    elseif (idx == 6) then

        text(client, "Once you enter the tactics, it is very difficult to leave. Please think it over. I do not want you to die from them.")
        link(client, "I made up my mind.", 3)
        link(client, "I changed my mind.", 255)
        pic(client, 9)
        create(client)

    end

end
