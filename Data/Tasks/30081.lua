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

function processTask30081(client, idx)
    name = "CoachLin"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "Ref.Letter.Li") and (getMoney(client) >= 0) then

            if getLevel(client) < 16 then

                text(client, "Since Coach Li introduced you, I must try my best to help you. Please come to me when you reach level 16.")
                link(client, "OK.", 255)
                pic(client, 7)
                create(client)

            else

                text(client, "Since you have come, I shall ask you a few questions. If you answer correctly, I will give you a reward.")
                link(client, "Ok. Go ahead.", 1)
                link(client, "Too troublesome.", 255)
                pic(client, 7)
                create(client)

            end

        else

         text(client, "It seems each new generation excels the last one.")
         link(client, "Of course.", 255)
         pic(client, 7)
         create(client)

        end

    elseif (idx == 1) then

        if hasTaskItem(client, "Ref.Letter.Li") and (getMoney(client) >= 0) then

            action = randomAction(client, 1, 8)
            if action == 1 then
                text(client, "When can a player get promoted for the second time?")
                link(client, "Level 40", 2)
                link(client, "Level 30", 3)
                link(client, "Level 15", 3)
                link(client, "Level 70", 3)
                pic(client, 7)
                create(client)
            elseif action == 2 then
                text(client, "Who teleports you into the Training Ground of Twin City?")
                link(client, "SouthBoxer", 3)
                link(client, "BoxerHuang", 3)
                link(client, "NorthBoxer", 3)
                link(client, "Boxer", 2)
                pic(client, 7)
                create(client)
            elseif action == 3 then
                text(client, "How many items can you store in a warehouse?")
                link(client, "15", 3)
                link(client, "20", 2)
                link(client, "25", 3)
                link(client, "30", 3)
                pic(client, 7)
                create(client)
            elseif action == 4 then
                text(client, "When can Trojan be equipped with two-handed weapons?")
                link(client, "Level 15", 3)
                link(client, "Level 30", 3)
                link(client, "Level 40", 2)
                link(client, "Level 70", 3)
                pic(client, 7)
                create(client)
            elseif action == 5 then
                text(client, "When can a player get promoted for the second time?")
                link(client, "Level 40", 2)
                link(client, "Level 30", 3)
                link(client, "Level 15", 3)
                link(client, "Level 70", 3)
                pic(client, 7)
                create(client)
            elseif action == 6 then
                text(client, "Who teleports you into the Training Ground of Twin City?")
                link(client, "SouthBoxer", 3)
                link(client, "BoxerHuang", 3)
                link(client, "NorthBoxer", 3)
                link(client, "Boxer", 2)
                pic(client, 7)
                create(client)
            elseif action == 7 then
                text(client, "How many items can you store in a warehouse?")
                link(client, "15", 3)
                link(client, "20", 2)
                link(client, "25", 3)
                link(client, "30", 3)
                pic(client, 7)
                create(client)
            elseif action == 8 then
                text(client, "When can Trojan be equipped with two-handed weapons?")
                link(client, "Level 15", 3)
                link(client, "Level 30", 3)
                link(client, "Level 40", 2)
                link(client, "Level 70", 3)
                pic(client, 7)
                create(client)
            end


        end

    elseif (idx == 2) then

        if hasTaskItem(client, "Ref.Letter.Li") and (getMoney(client) >= 0) then

            text(client, "Good. Here is an item for you. This item will be useful to you.")
            link(client, "Thanks a lot.", 4)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 3) then

        if hasTaskItem(client, "Ref.Letter.Li") and (getMoney(client) >= 0) then

            text(client, "Sorry, your answer is wrong. Please come again when you learn more about Conquer.")
            link(client, "OK.", 255)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 4) then

        if hasTaskItem(client, "Ref.Letter.Li") and (getMoney(client) >= 0) then

            spendItem(client, 721115, 1)
            if (rand(client, 2) < 1) then

                action = randomAction(client, 1, 8)
                if action == 1 then
                    awardItem(client, "410036", 1)
                elseif action == 2 then
                    awardItem(client, "420036", 1)
                elseif action == 3 then
                    awardItem(client, "560036", 1)
                elseif action == 4 then
                    awardItem(client, "430036", 1)
                elseif action == 5 then
                    awardItem(client, "450036", 1)
                elseif action == 6 then
                    awardItem(client, "490036", 1)
                elseif action == 7 then
                    awardItem(client, "460036", 1)
                elseif action == 8 then
                    awardItem(client, "561036", 1)
                end


            else

                action = randomAction(client, 1, 8)
                if action == 1 then
                    awardItem(client, "481036", 1)
                elseif action == 2 then
                    awardItem(client, "510036", 1)
                elseif action == 3 then
                    awardItem(client, "480036", 1)
                elseif action == 4 then
                    awardItem(client, "440036", 1)
                elseif action == 5 then
                    awardItem(client, "580036", 1)
                elseif action == 6 then
                    awardItem(client, "540036", 1)
                elseif action == 7 then
                    awardItem(client, "440026", 1)
                elseif action == 8 then
                    awardItem(client, "421026", 1)
                end


            end

        end

    end

end
