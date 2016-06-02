--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:55 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask600055(client, idx)
    name = "Starlit"
    face = 1

    if (idx == 0) then

        text(client, "One star in the sky represents a person. When a star falls, one person will disappear. Then I will make up a new one.")
        link(client, "You are great.", 255)
        link(client, "Can you give me a star?", 1)
        pic(client, 6)
        create(client)

    elseif (idx == 1) then

        text(client, "Every star represents a person on the earth. How can I give it to you? If you have nothing special, please do not disturb me.")
        link(client, "I am leaving.", 255)
        link(client, "What are you busy with?", 2)
        pic(client, 6)
        create(client)

    elseif (idx == 2) then

        text(client, "Couples rely on each other as their stars in the sky do. When they do not get along well with each other, I shall??")
        link(client, "What will you do?", 3)
        pic(client, 6)
        create(client)

    elseif (idx == 3) then

        text(client, "I will make their stars move apart until they can no longer shine in each other. Then they will break up finally.")
        link(client, "Why are you so cruel?", 4)
        link(client, "I see.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 4) then

        text(client, "I do not take them apart unless they have suffered a lot from their marriage and decide to end up their relationship.")
        link(client, "Can you help me divorce?", 5)
        link(client, "I see.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 5) then

        text(client, "If your marriage left you nothing but regret and pain, then bring a meteor and a meteor tear, I can help you out.")
        link(client, "Why are they needed?", 6)
        link(client, "I love my spouse.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 6) then

        text(client, "A star stays in the sky, then the person can revive. If it falls down as a meteor, he/she cannot, and I will get it back.")
        link(client, "What is Meteor Tear for?", 7)
        pic(client, 6)
        create(client)

    elseif (idx == 7) then

        text(client, "The true love will move a meteor to tears, thus Meteor Tear is formed. I do not take Meteor Tear for myself.")
        link(client, "For whom then?", 8)
        pic(client, 6)
        create(client)

    elseif (idx == 8) then

        text(client, "I shall give it to your spouse as comfort. I hope he/she will know the world is still full of love, and live happily.")
        link(client, "I see.", 255)
        link(client, "Get divorced.", 9)
        pic(client, 6)
        create(client)

    elseif (idx == 9) then

        if isMarried(client) then

            text(client, "The lovers should care for each other. Do not draw a conclusion in haste. You had better think it over.")
            link(client, "I have made up my mind.", 10)
            link(client, "Let me think it over.", 255)
            pic(client, 6)
            create(client)

        else

            text(client, "You are still single. Do not play such joke on me.")
            link(client, "Just a joke.", 255)
            pic(client, 6)
            create(client)

        end

    elseif (idx == 10) then

        text(client, "Are you sure that you want to leave your lover?")
        link(client, "Yeah.", 11)
        link(client, "I changed my mind.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 11) then

        text(client, "Since you have made up your mind, I shall help you and take away your Meteor Tear and Meteor.")
        link(client, "Thanks.", 12)
        link(client, "I changed my mind.", 255)
        pic(client, 6)
        create(client)

    elseif (idx == 12) then

        if hasItem(client, 1088001, 1) then

            if hasItem(client, 1088002, 1) then

                spendItem(client, 1088001, 1)
                spendItem(client, 1088002, 1)
                text(client, "You divorced your spouse. I hope you can think it over before you get married next time. Do not come for divorce any more.")
                link(client, "Thanks.", 255)
                pic(client, 6)
                create(client)
                divorce(client)

            else

                text(client, "Sorry, you do not have a meteor and meteor tear.")
                link(client, "I see.", 255)
                pic(client, 6)
                create(client)

            end

        else

            text(client, "Sorry, you do not have a meteor and meteor tear.")
            link(client, "I see.", 255)
            pic(client, 6)
            create(client)

        end

    end

end
