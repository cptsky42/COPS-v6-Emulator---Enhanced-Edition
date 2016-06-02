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

function processTask600063(client, idx)
    name = "Roger"
    face = 1

    if (idx == 0) then

        if hasItem(client, 723003, 1) then

            text(client, "If you get the unidentified letter, you can take my letter with you to ask my good friend William to identify it.")
            link(client, "Oh, I see.", 255)
            pic(client, 25)
            create(client)

        else

            text(client, "Where do I begin to tell a story of true love?")
            link(client, "Have a cup of tea first.", 1)
            pic(client, 25)
            create(client)

        end

    elseif (idx == 1) then

        if hasItem(client, 723002, 1) then

            if hasItem(client, 723001, 1) then

                text(client, "Isn`t it Rachel`s handkerchief? How do you get it? Why is it so dirty? Why do not you treasure it?")
                link(client, "Inquire about MoonGem.", 255)
                link(client, "It is from GossiperWang.", 2)
                pic(client, 25)
                create(client)

            else

                text(client, "I am wondering what kind of flower you are carrying. It gives out mystic scent.")
                link(client, "It is balsamine.", 3)
                link(client, "Keep it secret.", 255)
                pic(client, 25)
                create(client)

            end

        else

            if hasItem(client, 723001, 1) then

                text(client, "Isn`t it Rachel`s Hankchief? How can you get it and why is it so dirty?")
                link(client, "I do not know.", 4)
                link(client, "I did it.", 255)
                pic(client, 25)
                create(client)

            else

                text(client, "It is hot and I am so tired. So it goes out of business today.")
                link(client, "You are so lazy.", 255)
                pic(client, 25)
                create(client)

            end

        end

    elseif (idx == 2) then

        text(client, "You mean GossiperWang? Please give it to me. It will remind me of her. Do you have any requirements?")
        link(client, "I cannot give you.", 255)
        link(client, "Here you are.", 5)
        pic(client, 25)
        create(client)

    elseif (idx == 3) then

        text(client, "Sigh! Even the magic balsamine can`t help. I am crazy about Rachel and can hardly get sleep without seeing her.")
        link(client, "Poor guy.", 255)
        link(client, "How can I help you?", 6)
        pic(client, 25)
        create(client)

    elseif (idx == 4) then

        text(client, "Please give it to me, I will give you 30,000 silver. It is my lover`s hankerchief.")
        link(client, "No, I need it too.", 255)
        link(client, "Alrigth, here you are.", 7)
        pic(client, 25)
        create(client)

    elseif (idx == 5) then

        text(client, "The painting on the handkerchief is the map of the place where to find Balsamine. It is so considerate of him.")
        text(client, "Thanks for the Balsamine and the handkerchief. Tell me what you need, I will do my best to help you.")
        link(client, "Where is the MoonGem?", 8)
        pic(client, 25)
        create(client)

    elseif (idx == 6) then

        text(client, "I heard that Balsamine is good for health. Could you give it to Rachel? She lives in the furthest village of Bird Island.")
        link(client, "It is ok.", 255)
        link(client, "Sorry, I am busy.", 255)
        pic(client, 25)
        create(client)

    elseif (idx == 7) then

        spendItem(client, 723001, 1)
        gainMoney(client, 30000)

    elseif (idx == 8) then

        text(client, "I heard that a master wrote a Moon Letter to indicate its location. Unfortunately, the letter has lost for years.")
        text(client, "The other day, I happened to know that King of Blade Ghost in the desert once collected it.")
        link(client, "How to get it?", 9)
        pic(client, 25)
        create(client)

    elseif (idx == 9) then

        text(client, "The king of Blade Ghost is very foxy. He asked an artificer to forge one. They are alike and hard to be identified.")
        text(client, "You may get it if you defeat him. I will recommend you to William in PhoenixCastle to distinguish the authenticity of the lette")
        link(client, "Thank you, sir.", 10)
        link(client, "I will give it up.", 255)
        pic(client, 25)
        create(client)

    elseif (idx == 10) then

        spendItem(client, 723001, 1)
        spendItem(client, 723002, 1)
        awardItem(client, "723003", 1)

    end

end
