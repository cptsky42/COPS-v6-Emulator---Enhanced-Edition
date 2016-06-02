--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:58 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask600068(client, idx)
    name = "OldGeneral"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "PureGem") and (getMoney(client) >= 0) then

            text(client, "MoonGem is very good for Tao to practice his spells. You should make good use of it.")
            link(client, "Thanks. I will.", 255)
            pic(client, 91)
            create(client)

        else

         if hasItem(client, 723009, 1) then

             text(client, "This is the MoonLetter which has lost for years. It seems that William has got the meaning in the letter.")
             text(client, "The moon above Ape Mountain and in the midnight? It must indicate the position of the Phython.")
             link(client, "It sounds reasonable.", 1)
             pic(client, 91)
             create(client)

         else

             if hasItem(client, 723006, 1) then

                 text(client, "It is MoonLetter. But I am old and my eyes are not good enough. Can you find someone to explain the letter?")
                 link(client, "Ok.", 255)
                 pic(client, 91)
                 create(client)

             else

                 if hasItem(client, 723010, 1) then

                     if hasItem(client, 723011, 1) then

                         text(client, "It is the heart of Phython. You are so brave to kill Phython. I admire you so much.")
                         text(client, "Now the force of the monsters is weakening and we will be in peace one day. I will give you a refined MoonGem.")
                         link(client, "Thanks a lot.", 2)
                         pic(client, 91)
                         create(client)

                     else

                         text(client, "You can find Emily outside the city between 11:00 pm and 1:00 am. You must take care.")
                         link(client, "Thanks.", 255)
                         pic(client, 91)
                         create(client)

                     end

                 else

                     text(client, "Hi, my friend! Could you do me a favor?")
                     link(client, "Sure. What is it?", 3)
                     link(client, "Sorry, I am busy.", 255)
                     pic(client, 91)
                     create(client)

                 end

             end

         end

        end

    elseif (idx == 1) then

        text(client, "It is a pity that my soldiers can not go out to kill the Phython for they have to guard the city. Can you help me to kill it?")
        text(client, "If you help me out, I will give you a MoonGem on behalf of the citizens.")
        link(client, "I will have a try.", 4)
        link(client, "No, it is too dangerous.", 255)
        pic(client, 91)
        create(client)

    elseif (idx == 2) then

        spendItem(client, 723010, 1)
        spendItem(client, 723011, 1)
        awardItem(client, "700062", 1)

    elseif (idx == 3) then

        text(client, "It starts from Moon Gem. 500 years ago, my master faced down an ape to guard the city. That is why it was named Ape Mountain.")
        text(client, "He left four supernatural gems to guard the four cities when he advanced to an immortal. And Moon Gem safeguarded this city.")
        link(client, "It is great, isn`t it?", 5)
        pic(client, 91)
        create(client)

    elseif (idx == 4) then

        text(client, "Phython has a heeler called Emily. She usually dressed up like a beauty outside the city. She may send you to phython`s cave.")
        text(client, "For fear that Phython disguises as you to get the Gem, take his heart and this AidToken to me after killing it.")
        link(client, "Thanks for reminding.", 6)
        link(client, "I will give it up.", 255)
        pic(client, 91)
        create(client)

    elseif (idx == 5) then

        text(client, "Yeah. But ever since endless monsters were attempting to snatch the gem. We can always defeat them and guard the city.")
        text(client, "Recently, a Python plotted Emily to entice my son. He almost got it. Then he called in monsters to besiege the city.")
        text(client, "I sent strong armies to raise the siege of it. But there was no news afterwards. It`s a pity we do not know where his den is.")
        link(client, "What can I do for you?", 7)
        link(client, "It is bad. I must go.", 255)
        pic(client, 91)
        create(client)

    elseif (idx == 6) then

        spendItem(client, 723009, 1)
        awardItem(client, "723010", 1)

    elseif (idx == 7) then

        text(client, "My master once left a secret letter and told me to resort to it when we encounter troubles. But the letter was lost years ago.")
        text(client, "If I could find the MoonLetter, it would be much easier.")
        link(client, "Let me help you.", 255)
        pic(client, 91)
        create(client)

    end

end
