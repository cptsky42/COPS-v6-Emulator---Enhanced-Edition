--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:48 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30087(client, idx)
    name = "Druggist"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "Bandage") and (getMoney(client) >= 0) then

            text(client, "You have got Bandage, please go away and don`t bother me.")
            link(client, "Alright.", 255)
            pic(client, 2)
            create(client)

        elseif hasTaskItem(client, "ShopList") and (getMoney(client) >= 0) then

            text(client, "Three Ginsengs are needed to refine Invigorant.")
            link(client, "Here you are.", 1)
            link(client, "I will get it ready.", 255)
            pic(client, 2)
            create(client)

        else

         text(client, "I should concentrate on refining drugs. Please leave me alone if you do not have anything important to do here.")
         link(client, "Alright.", 255)
         pic(client, 2)
         create(client)

        end

    elseif (idx == 1) then

        if hasTaskItem(client, "ShopList") and (getMoney(client) >= 0) then

            if hasItem(client, 1002010, 3) then

                spendItem(client, 721118, 1)
                spendItem(client, 1002010, 3)
                awardItem(client, "721119", 1)

            else

                text(client, "You don`t have enough Ginsengs. Please buy more.")
                link(client, "You are so troublesome.", 255)
                pic(client, 2)
                create(client)

            end

        end

    end

end
