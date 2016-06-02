--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 4/20/2015 8:20:45 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask300000(client, idx)
    name = "Shelby"
    face = 1

    if (idx == 0) then

        text(client, "We hope all can help each other. If you power level the newbies, we may reward you DragonBalls or Meteors. Are you interested?")
        link(client, "Tell me more details.", 1)
        link(client, "Check my virtue points.", 2)
        link(client, "Claim Prize.", 3)
        link(client, "Just passing by.", 255)
        pic(client, 29)
        create(client)

    elseif (idx == 1) then

        text(client, "If you are above level 70 and try to power level the newbies (at least 20 levels lower than you), you may gain virtue points.")
        link(client, "What are the virtue points?", 4)
        pic(client, 29)
        create(client)

    elseif (idx == 2) then

        text(client, "You currently have " .. getVirtue(client) .. " virtue points.")
        link(client, "Thanks.", 255)
        pic(client, 29)
        create(client)

    elseif (idx == 3) then

        text(client, "What prize do you prefer?")
        link(client, "Meteor", 5)
        link(client, "DragonBall.", 6)
        link(client, "Let me think it over", 255)
        pic(client, 29)
        create(client)

    elseif (idx == 4) then

        text(client, "The more newbies you power level, the more virtue points you gain. I shall give you a good reward for a certain virtue points.")
        link(client, "How can I gain virtue points?", 7)
        link(client, "What prize can I expect?", 8)
        pic(client, 29)
        create(client)

    elseif (idx == 5) then

        if getVirtue(client) < 5000 then

            text(client, "Sorry, you do not have the required virtue points.")
            link(client, "I see.", 255)
            pic(client, 29)
            create(client)

        else

            awardItem(client, "1088001", 1)
            spendVirtue(client, 5000)

        end

    elseif (idx == 6) then

        if getVirtue(client) < 270000 then

            text(client, "Sorry, you do not have the required virtue points.")
            link(client, "I see.", 255)
            pic(client, 29)
            create(client)

        else

            awardItem(client, "1088000", 1)
            spendVirtue(client, 270000)

        end

    elseif (idx == 7) then

        text(client, "Once the newbies are one level up, the team captain can gain virtue points accordingly.")
        link(client, "I see.", 255)
        pic(client, 29)
        create(client)

    elseif (idx == 8) then

        text(client, "I shall reward you a DragonBall for 270'000 virtue points or a meteor for 5000 virtue points.")
        link(client, "I see.", 255)
        pic(client, 29)
        create(client)

    end

end
