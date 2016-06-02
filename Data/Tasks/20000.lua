--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:43 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask20000(client, idx)
    name = "Shirley"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721211, 1) then

            text(client, "When I was young, I liked Windbell very much. My brother often told me that I should take care of myself whatever happened.")
            link(client, "Wish you happy every day.", 1)
            pic(client, 109)
            create(client)

        else

            if hasItem(client, 721210, 1) then

                text(client, "If my brother has been dead, Would you please bring my Jade back.")
                link(client, "Okay.", 255)
                pic(client, 109)
                create(client)

            else

                if getLevel(client) < 50 then

                    text(client, "How nice it would be if there is no wars in this world!")
                    link(client, "I hope so.", 255)
                    pic(client, 109)
                    create(client)

                else

                    text(client, "If I cannot hear from my brother, I would rather die than live lonely.")
                    link(client, "Can I help you?", 2)
                    link(client, "Oh, another poor girl.", 255)
                    pic(client, 109)
                    create(client)

                end

            end

        end

    elseif (idx == 1) then

        text(client, "I must live happily as my brother wished. Thanks for your great help. I give you my favorite earring. Do you like it?")
        link(client, "Yeah. I like it best.", 3)
        link(client, "Thanks. I have one, too.", 255)
        pic(client, 109)
        create(client)

    elseif (idx == 2) then

        text(client, "My parents died when I was a child. My brother brought me up. I have not heard from him since he joined the army last year.")
        link(client, "Can I help you?", 4)
        link(client, "I am helpless.", 255)
        pic(client, 109)
        create(client)

    elseif (idx == 3) then

        if hasItem(client, 721210, 1) then

            spendItem(client, 721211, 1)
            spendItem(client, 721210, 1)
            awardItem(client, "117346", 1)

        else

            text(client, "You have not brought my Jade back? That Jade and Windbell are my favorite. If I lose them, I cannot live.")
            link(client, "I got to get it.", 255)
            pic(client, 109)
            create(client)

        end

    elseif (idx == 4) then

        text(client, "My brother`s name is Ryan. He gave me this Jade to celebrate my 10th birthday. Would you please take it to find him for me.")
        link(client, "Sure. I must find him.", 5)
        link(client, "Sorry, I am too busy.", 255)
        pic(client, 109)
        create(client)

    elseif (idx == 5) then

        awardItem(client, "721210", 1)

    end

end
