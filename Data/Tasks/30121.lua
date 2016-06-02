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

function processTask30121(client, idx)
    name = "Hades"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721152, 1) then

            text(client, "Hope Lauren will come back soon.")
            link(client, "Wish you good luck.", 255)
            pic(client, 73)
            create(client)

        else

            if hasItem(client, 721151, 1) then

                text(client, "I know where WaterRing is. It belongs to Lauren, a human being, and it refuses to return to me. Because of it,")
                text(client, "Lauren was enchanted and tortured by illness. If only she dies, it can come back to me.")
                link(client, "Are you going to kill her?", 1)
                link(client, "Oh. Excuse me for a moment.", 255)
                pic(client, 73)
                create(client)

            else

                text(client, "There is only one thing that can reverse the universe. That is love.")
                link(client, "It sounds reasonable.", 255)
                pic(client, 73)
                create(client)

            end

        end

    elseif (idx == 1) then

        text(client, "On the opposite, I went to the human world and bumpted into Lauren. I attempted to cheat WaterRing out of her. But")
        text(client, "things went agaist my will. I fell in love with her. In order to prolong her life, I went back to Ghostdom and")
        text(client, "modified the Roll of Metempsychosis. Just before I left, Lauren was taken by Tientsin from the kingdom of god.")
        link(client, "Why not hurry to rescue her?", 2)
        pic(client, 73)
        create(client)

    elseif (idx == 2) then

        text(client, "I wish I could. A ghost does not have the access to the kingdom of god. If you want WaterRing, take my letter")
        text(client, "to DevineArtisan, and he will give you the Seal. With it, you can find Lauren and help her to take off WaterRing.")
        text(client, "Since I have changed the Roll of Metempsychosis, Tientsin can do nothing but set Lauren free if DevineArtisan gets WaterRing.")
        link(client, "I see. Where is Lauren?", 3)
        link(client, "Sorry. I have got to go.", 255)
        pic(client, 73)
        create(client)

    elseif (idx == 3) then

        text(client, "She was enjailed in the deepest of Violet Jail, the main jail of Tientsin.")
        link(client, "I shall find Tientsin first.", 4)
        link(client, "Oh, I have to go.", 255)
        pic(client, 73)
        create(client)

    elseif (idx == 4) then

        text(client, "Yeah, Tienstin stays at the peak of Dreamland every day from 13:00 to 18:00. If you get the passport, you can enter Violet Jail")
        link(client, "Ok. I will help you out.", 5)
        link(client, "Oh, I am busy at the time.", 255)
        pic(client, 73)
        create(client)

    elseif (idx == 5) then

        spendItem(client, 721151, 1)
        awardItem(client, "721152", 1)

    end

end
