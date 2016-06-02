--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:40 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask10004(client, idx)
    name = "BirthManager"
    face = 1

    if (idx == 0) then

        if getProfession(client) == 100 then

            text(client, "There are NPCs standing around the town. You may click on them to sell, buy, repair items, learn info, get promoted??")
            link(client, "How about my job?", 1)
            link(client, "Consult others.", 255)
            pic(client, 6)
            create(client)

        else

            if getProfession(client) == 20 then

                text(client, "There are NPCs standing around the town. You may click on them to sell, buy, repair items, learn info, get promoted??")
                link(client, "How about my job?", 2)
                link(client, "Consult others.", 255)
                pic(client, 6)
                create(client)

            else

                if getProfession(client) == 10 then

                    text(client, "There are NPCs standing around the town. You may click on them to sell, buy, repair items, learn info, get promoted??")
                    link(client, "How about my job?", 3)
                    link(client, "Consult others.", 255)
                    pic(client, 6)
                    create(client)

                else

                    if getProfession(client) == 40 then

                        text(client, "There are NPCs standing around the town. You may click on them to sell, buy, repair items, learn info, get promoted??")
                        link(client, "How about my job?", 4)
                        link(client, "Consult others.", 255)
                        pic(client, 6)
                        create(client)

                    else

                        if getProfession(client) == 30 then

                            text(client, "There are NPCs standing around the town. You may click on them to sell, buy, repair items, learn info, get promoted??")
                            link(client, "How about my job?", 5)
                            link(client, "Consult others.", 255)
                            pic(client, 6)
                            create(client)

                        else

                            text(client, "I am so pleased to learn that you have achieved such great success in a very short period. Congrats!")
                            link(client, "Thanks.", 255)
                            pic(client, 6)
                            create(client)

                        end

                    end

                end

            end

        end

    elseif (idx == 1) then

        text(client, "You have chosen a great class. Taoists are good at spells. When you are level 40, you may select fire or water path.")
        text(client, "You have chosen a great class. The merchants will tell you what the best items are. Ask Mr. Nosy and KnowItAll for more info.")
        link(client, "I see. Thanks.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 2) then

        text(client, "It`s a great class. Warrior prefers melee. Only warrior can wear shield and helmet. So warrior is the best defender.")
        text(client, "You have chosen a great class. The merchants will tell you what the best items are. Ask Mr. Nosy and KnowItAll for more info.")
        link(client, "I see. Thanks.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 3) then

        text(client, "You made the right choice. Trojan is able to use two handed weapons and delivers more damages. Trojan has higher HP.")
        text(client, "You have chosen a great class. The merchants will tell you what the best items are. Ask Mr. Nosy and KnowItAll for more info.")
        link(client, "I see. Thanks.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 4) then

        text(client, "Archers are good at bow and arrow. They are fast, adept at long range attack and rapid shoot. They can also fly.")
        text(client, "You have chosen a great class. The merchants will tell you what the best items are. Ask Mr. Nosy and KnowItAll for more info.")
        link(client, "I see. Thanks.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 5) then

        text(client, "With rich skills and unique mana, Knight always takes advantages of either distance or close combat.")
        text(client, "You have chosen a great class. The merchants will tell you what the best items are. Ask Mr. Nosy and KnowItAll for more info.")
        link(client, "I see. Thanks.", 255)
        pic(client, 6)
        create(client)

    end

end
