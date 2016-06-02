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

function processTask600002(client, idx)
    name = "Joe"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "MeteorTear") and (getMoney(client) >= 0) then

            text(client, "I am so delighted to hear that Minner can understand me. I shall love and miss her wherever I go.")
            link(client, "I shall try to help you.", 255)
            pic(client, 10)
            create(client)

        elseif hasTaskItem(client, "SadMeteor") and (getMoney(client) >= 0) then

            text(client, "Thanks a lot. Please kindly give this meteor to Minner. I hope she can understand me and wish her happy all her life.")
            link(client, "I hope so.", 255)
            pic(client, 10)
            create(client)

        else

         text(client, "It is so hot. It would be very nice If I have a bottle of wine. Can you give me some wine?")
         link(client, "I have a bottle of Amrita.", 1)
         link(client, "I do not have either.", 255)
         pic(client, 10)
         create(client)

        end

    elseif (idx == 1) then

        if hasItem(client, 1000030, 1) then

            if hasItem(client, 1088001, 1) then

                if hasItem(client, 721001, 1) then

                    text(client, "Thank you. You are very kind. I often have no food and water when I travel around. Where did you get this guardian star?")
                    link(client, "It is from Minner.", 2)
                    link(client, "I picked it on the road.", 3)
                    pic(client, 10)
                    create(client)

                else

                    text(client, "I cannot take your Amrita. There is little water in the vast desert. You may feel thirsty and need it later.")
                    link(client, "See you then.", 255)
                    pic(client, 10)
                    create(client)

                end

            else

                text(client, "I cannot take your Amrita. There is little water in the vast desert. You may feel thirsty and need it later.")
                link(client, "See you then.", 255)
                pic(client, 10)
                create(client)

            end

        else

            text(client, "I cannot take your Amrita. There is little water in the vast desert. You may feel thirsty and need it later.")
            link(client, "See you then.", 255)
            pic(client, 10)
            create(client)

        end

    elseif (idx == 2) then

        text(client, "So she sent you to look for me. I have left so long. She must be heartbroken.")
        link(client, "Why did you leave her?", 4)
        pic(client, 10)
        create(client)

    elseif (idx == 3) then

        text(client, "I must have hurt her a lot. She used to carry this bag wherever she goes. I hope she can forget me and be happy all her life.")
        link(client, "I got to go. Bye.", 255)
        pic(client, 10)
        create(client)

    elseif (idx == 4) then

        text(client, "We love each other deeply. I wish I could stay with her, but my life dream is to travel around. I cannot give her a warm home.")
        link(client, "You are right.", 5)
        pic(client, 10)
        create(client)

    elseif (idx == 5) then

        text(client, "What a nice meteor you have. I have promised Minner that I shall give her a meteor. Would you please give your meteor to me.")
        link(client, "Here you are.", 6)
        link(client, "I like it very much.", 255)
        pic(client, 10)
        create(client)

    elseif (idx == 6) then

        text(client, "Thanks. I shall engrave my love on this meteor. Please kindly give it to Minner. She will understand why I leave her.")
        link(client, "I shall give it to her.", 7)
        link(client, "Do not touch my meteor.", 255)
        pic(client, 10)
        create(client)

    elseif (idx == 7) then

        spendItem(client, 721001, 1)
        spendItem(client, 1088001, 1)
        awardItem(client, "721002", 1)

    end

end
