--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:49 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30102(client, idx)
    name = "VillageHead"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721127, 1) then

            text(client, "I know sometimes the feudal official is not trustful. But they are the only person we can ask for help from.")
            link(client, "Sorry, I am unable to help you though I hope I can.", 1)
            pic(client, 15)
            create(client)

        else

            if hasItem(client, 721129, 1) then

                text(client, "You are a wonderful guy to save our village. We are poor but please accept the money though it is not much.")
                link(client, "How can I accept your money?", 2)
                link(client, "Thank you.", 3)
                pic(client, 15)
                create(client)

            else

                if hasItems(client, 721125, 721126, 1) then

                    text(client, "Hope you can help us and we can use the water securely one day.")
                    link(client, "Wait for my good news.", 255)
                    pic(client, 15)
                    create(client)

                else

                    if getLevel(client) < 70 then

                        text(client, "This small village lies in the vast desert. We support ourselves by selling some stuff to the adventurers and travellers.")
                        link(client, "I see.", 255)
                        pic(client, 15)
                        create(client)

                    else

                        text(client, "Could you help me?")
                        link(client, "What`s wrong with you.", 4)
                        link(client, "Sorry. I am busy now.", 255)
                        pic(client, 15)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 1) then

        text(client, "Please accept the money though it is not much.")
        link(client, "Thank you.", 5)
        pic(client, 15)
        create(client)

    elseif (idx == 2) then

        if (rand(client, 200) < 1) then

            text(client, "Thanks. To express my appreciation, take it. It is left by a visitor and we have no use for it and hope it is useful to you.")
            link(client, "Thanks. I will take it away.", 6)
            pic(client, 15)
            create(client)

        else

            text(client, "You are so kind to help us. We will remember you forever.")
            link(client, "You are welcome.", 7)
            pic(client, 15)
            create(client)

        end

    elseif (idx == 3) then

        spendItem(client, 721129, 1)
        gainMoney(client, 5000)

    elseif (idx == 4) then

        text(client, "It lacks rain in desert. God has presented us a rich water source-MoonSpring in the west of our village.")
        link(client, "It is great, isn`t it?", 8)
        pic(client, 15)
        create(client)

    elseif (idx == 5) then

        spendItem(client, 721127, 1)
        gainMoney(client, 1000)

    elseif (idx == 6) then

        spendItem(client, 721129, 1)
        awardItem(client, "150116", 1)

    elseif (idx == 7) then

        spendItem(client, 721129, 1)

    elseif (idx == 8) then

        text(client, "But something unexpected happened soon. One day some StoneBandits occupied MoonSpring by force.")
        link(client, "And then?", 9)
        pic(client, 15)
        create(client)

    elseif (idx == 9) then

        text(client, "We should buy water from him. We can not afford it if it lasts for a long time.")
        link(client, "What can I do to help you?", 10)
        link(client, "It`s none of my business.", 255)
        pic(client, 15)
        create(client)

    elseif (idx == 10) then

        text(client, "Go teach them a lesson. Hope they won`t come here to bother us. Take something from them as keepsake.")
        link(client, "I will help you. Wait for my good news.", 11)
        pic(client, 15)
        create(client)

    elseif (idx == 11) then

        awardItem(client, "721126", 1)

    end

end
