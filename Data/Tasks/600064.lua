--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:56 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask600064(client, idx)
    name = "Rachel"
    face = 1

    if (idx == 0) then

        text(client, "I live in the western of the Yongtse River, you live in the eastern. Miss you but can not see you. We only share the same river")
        text(client, "When will the river stop flowing? When will the time stop? I only know that we miss each other.")
        link(client, "People are sensitive.", 255)
        link(client, "Why are you so sad?", 1)
        pic(client, 158)
        create(client)

    elseif (idx == 1) then

        if hasItem(client, 723002, 1) then

            if hasItem(client, 723001, 1) then

                text(client, "Sigh, my parents died early. Nobody cares me except for Roger. But he fell ill because of insomnia. Your Balsamine can cure him")
                link(client, "Right.", 2)
                link(client, "It is only usual weed.", 255)
                pic(client, 158)
                create(client)

            else

                text(client, "My parents died early. Nobody takes care of me except for Roger. But he got illed now. Is the herb in your hand Balsamine?")
                link(client, "Yeah.", 3)
                link(client, "It is only ordinary weed.", 255)
                pic(client, 158)
                create(client)

            end

        else

            if hasItem(client, 723001, 1) then

                text(client, "Isn`t it my handkerchief? How do you get it? Return it to me!")
                link(client, "Here you are.", 4)
                link(client, "It is still useful.", 255)
                pic(client, 158)
                create(client)

            else

                text(client, "Miss you but can not see you. We only share the same river.")
                link(client, "Well.", 255)
                pic(client, 158)
                create(client)

            end

        end

    elseif (idx == 2) then

        text(client, "The Hankerchief in your hand is mine. Where do you get it and can you return it to me?")
        link(client, "It is from GossiperWang.", 5)
        link(client, "It is still helpful.", 255)
        pic(client, 158)
        create(client)

    elseif (idx == 3) then

        text(client, "Rogger fell sick due to insomnia. I hear that it is good for curing insomnia. Could you sell it to me at 2,000 silver?")
        link(client, "Ok. It is a deal.", 6)
        link(client, "Let me think it over.", 255)
        pic(client, 158)
        create(client)

    elseif (idx == 4) then

        spendItem(client, 723001, 1)
        text(client, "Oh, my god. What did my brother print on it?")
        link(client, "You are his sister.", 255)
        pic(client, 158)
        create(client)

    elseif (idx == 5) then

        text(client, "Oh, the map shows where you can find Balsamine. Give me Balsamine and Hankerchief and I will give you 2,000 silver.")
        link(client, "No problem.", 7)
        link(client, "I won`t sell it.", 255)
        pic(client, 158)
        create(client)

    elseif (idx == 6) then

        spendItem(client, 723002, 1)
        gainMoney(client, 2000)

    elseif (idx == 7) then

        spendItem(client, 723001, 1)
        spendItem(client, 723002, 1)
        gainMoney(client, 2000)

    end

end
