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

function processTask600060(client, idx)
    name = "Mr.Pine"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "Letter.Pine") and (getMoney(client) >= 0) then

            text(client, "Take my recommendation to Gossiper Wang in Bird Island, and he will tell you more about the Moon Gem.")
            link(client, "Ok, thanks.", 255)
            pic(client, 92)
            create(client)

        else

         if hasItem(client, 152126, 1) then

             text(client, "Wow, you have a refined Bone Bracelet! If you give it to me, I will tell you the clue of Moon Gem.")
             link(client, "Ok, It is a deal.", 1)
             link(client, "Sorry, I need it too.", 255)
             pic(client, 92)
             create(client)

         else

             text(client, "Brigt moon is where my mind lies. Although you are far away, I know that I will have it one day.")
             link(client, "Why do you like it?", 2)
             link(client, "You are dreaming.", 255)
             pic(client, 92)
             create(client)

         end

        end

    elseif (idx == 1) then

        text(client, "Thanks for your generous help. You are a good guy. I hope that we will defeat the monsters together one day.")
        text(client, "My friend GossiperWang in BirdIsland is well-informed. You may get something about Moon Gem from him with my letter.")
        link(client, "I will pay him a visit.", 3)
        link(client, "Sorry, I am busy.", 255)
        pic(client, 92)
        create(client)

    elseif (idx == 2) then

        text(client, "It is not the moon in the sky. It is a precious stone which possesses the power of controlling the evils.")
        text(client, "If the Taoist has it socketed in his equipment, it will give him extra spell experience.")
        link(client, "Wow, it is marvelous.", 4)
        link(client, "I do not believe it.", 255)
        pic(client, 92)
        create(client)

    elseif (idx == 3) then

        spendItem(client, 152126, 1)
        awardItem(client, "723000", 1)

    elseif (idx == 4) then

        text(client, "Yes. It is what I am dreaming about as the rest of our Taoists. My master obtained it after he killed five powerful monsters.")
        text(client, "It is a pity that I did not see the heroic scene with my own eyes. Sigh!")
        link(client, "Yes, it is.", 5)
        link(client, "I have got to leave.", 255)
        pic(client, 92)
        create(client)

    elseif (idx == 5) then

        text(client, "However, the evils are getting the upper hand. The world of Conquer is infested with the evil monsters.")
        text(client, "I wish I could rescue the people from the suffering. But it is difficult to achieve that without support.")
        link(client, "Let me help you.", 6)
        link(client, "I have got to leave.", 255)
        pic(client, 92)
        create(client)

    elseif (idx == 6) then

        text(client, "Great! I need a refined Bone Bracelet badly to defeat the monsters outside the village, and RichmanZhang in Desert City has it.")
        text(client, "If you can help me get one from him, I will tell you more about the moon gem.")
        link(client, "I will go to find him.", 255)
        link(client, "I am not interested.", 255)
        pic(client, 92)
        create(client)

    end

end
