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

function processTask30100(client, idx)
    name = "MountainKing"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "SnakemanPro.") and (getMoney(client) >= 0) then

            text(client, "You have got rid of our big trouble. Please take the money as rewards.")
            link(client, "Thank you.", 1)
            pic(client, 10)
            create(client)

        elseif hasTaskItem(client, "ChallengeLetter") and (getMoney(client) >= 0) then

            text(client, "Please be very careful, the snakemen are known for their cruelty.")
            link(client, "Do not worry about me.", 255)
            pic(client, 10)
            create(client)

        elseif hasTaskItem(client, "ToMountainKing") and (getMoney(client) >= 0) then

            text(client, "Located in a remote mountain, we cannot get support from the army. So the snakemen often harry us and make us in misery.")
            link(client, "What can I do for you?", 2)
            link(client, "Poor you.", 255)
            pic(client, 10)
            create(client)

        else

         text(client, "The area is dangerous. You`d better leave here right now if you have nothing special to do.")
         link(client, "I am not a coward.", 255)
         pic(client, 10)
         create(client)

        end

    elseif (idx == 1) then

        if hasTaskItem(client, "SnakemanPro.") and (getMoney(client) >= 0) then

            spendItem(client, 721130, 1)
            gainMoney(client, 3000)

        end

    elseif (idx == 2) then

        if hasTaskItem(client, "ToMountainKing") and (getMoney(client) >= 0) then

            text(client, "We do not have soldiers to send to defeat the snakemen. I would appreciate if you can help us defeat them.")
            link(client, "You can trust me.", 3)
            link(client, "It is dangerous.", 255)
            pic(client, 10)
            create(client)

        end

    elseif (idx == 3) then

        if hasTaskItem(client, "ToMountainKing") and (getMoney(client) >= 0) then

            spendItem(client, 721122, 1)
            awardItem(client, "721123", 1)

        end

    end

end
