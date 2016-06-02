--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:47 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30080(client, idx)
    name = "CoachLi"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "Ref.Letter.Li") and (getMoney(client) >= 0) then

            text(client, "Keep this in mind. Only when you reach level 15 can CaptainLi help you.")
            link(client, "I see.", 255)
            pic(client, 7)
            create(client)

        elseif hasTaskItem(client, "NameCard") and (getMoney(client) >= 0) then

            text(client, "We call him General Cai. He lives outside Twin City. Take my name card with you. He may reward you some experience.")
            link(client, "I trust you.", 1)
            pic(client, 7)
            create(client)

        else

         if getLevel(client) < 16 then

             text(client, "You seem to be new to the world of Conquer.")
             link(client, "Yes.", 2)
             pic(client, 7)
             create(client)

         else

             if getLevel(client) < 31 then

                 text(client, "The circle is beset with peril. You would come to grief if you are careless. The advice from others would be helpful to you.")
                 link(client, "I am experienced.", 255)
                 link(client, "Who will help me?", 3)
                 pic(client, 7)
                 create(client)

             else

                 text(client, "You are not newcomer. Go ahead. Hope you will be outstanding one day.")
                 link(client, "I shall try my best.", 255)
                 pic(client, 7)
                 create(client)

             end

         end

        end

    elseif (idx == 1) then

        if spendMoney(client, 1000) then

            awardItem(client, "721116", 1)

        else

            text(client, "Sorry, you do not have 1,000 silver.")
            link(client, "I see.", 255)
            pic(client, 7)
            create(client)

        end

    elseif (idx == 2) then

        text(client, "I am an elementary coach in Twin City. I suggest you practice outside Twin City, or you will be in danger.")
        link(client, "Thank you for your advice.", 4)
        link(client, "I fear nothing.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 3) then

        text(client, "I have a friend who has been in circle for many years. If you give me 1000 silver, I can recommend you to him.")
        link(client, "Here is my money.", 1)
        link(client, "You are cheating.", 255)
        pic(client, 7)
        create(client)

    elseif (idx == 4) then

        text(client, "I give you a reference letter. When you reach level 16, you may get something useful from Coach Lin in Phoenix Castle.")
        link(client, "Thanks a lot.", 5)
        pic(client, 7)
        create(client)

    elseif (idx == 5) then

        awardItem(client, "721115", 1)

    end

end
