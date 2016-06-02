--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:51 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30166(client, idx)
    name = "Shopboy"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721252, 1) then

            text(client, "Are there any other things I can do for you?")
            link(client, "No, thanks.", 255)
            pic(client, 46)
            create(client)

        else

            if hasItem(client, 721251, 1) then

                text(client, "What can I do for you?")
                link(client, "Inquire about escort team.", 1)
                link(client, "No. Thanks.", 255)
                pic(client, 46)
                create(client)

            else

                text(client, "It is hot and dry in the desert. You can take a rest and have a cup of tea here.")
                link(client, "Thank you.", 255)
                pic(client, 46)
                create(client)

            end

        end

    elseif (idx == 1) then

        text(client, "You are asking the right person. At the time, more than 100 guards arrived here. But they were killed the following day.")
        link(client, "Did you find anything?", 2)
        link(client, "It is terrible.", 255)
        pic(client, 46)
        create(client)

    elseif (idx == 2) then

        text(client, "Let me think. I found a bloody blade in the well a couple of days ago. I don`t know whether it relates to the case or not.")
        link(client, "Show me the blade?", 3)
        link(client, "I don`t think it helful.", 255)
        pic(client, 46)
        create(client)

    elseif (idx == 3) then

        text(client, "If you give me 10,000 silver. I will send you the blade.")
        link(client, "Ok, give me the blade.", 4)
        link(client, "Forget it.", 255)
        pic(client, 46)
        create(client)

    elseif (idx == 4) then

        if getMoney(client) < 10000 then

            text(client, "Come here when you have enough money.")
            link(client, "Ok.", 255)
            pic(client, 46)
            create(client)

        else

            if spendMoney(client, 10000) then

                awardItem(client, "721252", 1)
                text(client, "You are generous. I will tell you another clue. Miss Roy in the maple forest is well-informed and knows much about ")
                text(client, "weapons. You may speak to her for more information.")
                link(client, "I will go right away.", 255)
                pic(client, 46)
                create(client)

            else

                text(client, "Come here when you have enough money.")
                link(client, "Ok.", 255)
                pic(client, 46)
                create(client)

            end

        end

    end

end
