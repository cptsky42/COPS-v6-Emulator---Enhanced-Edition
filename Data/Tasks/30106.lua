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

function processTask30106(client, idx)
    name = "David"
    face = 1

    if (idx == 0) then

        text(client, "My five brothers and I guard the arena in turn . We take pleasure in watching the PK Tournament and improving our skills.")
        link(client, "That is great!", 1)
        link(client, "Just passing by.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 1) then

        text(client, "My brothers and I want to watch the PK Tournament. but we are too busy to get some tickets from pheasants. Can you help us?")
        link(client, "Sure.", 2)
        link(client, "Sorry. I am too busy.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 2) then

        text(client, "Thanks a lot. If you give me 6 tickets, I shall pay you 6000 silvers. Are you ready to give your tickets to me?")
        link(client, "Yeah, here you are.", 3)
        link(client, "Sorry, I need them, too.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 3) then

        if hasItem(client, 710001, 6) then

            text(client, "Then I will take away your six tickets and pay you 6000 silver.")
            link(client, "Thanks.", 4)
            pic(client, 7)
            create(client)

        else

            text(client, "Sorry, you do not have 6 tickets.")
            link(client, "I see.", 255)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 4) then

        spendItem(client, 710001, 6)
        gainMoney(client, 6000)

    end

end
