--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/21/2015 1:36:20 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function useItem725015(self, client)
    name = "DivineHare"
    face = 1

    if getProfession(client) == 132 then

        if getLevel(client) < 54 then

            text(client, "Only Water Taoists can learn this spell after they reach level 54.")
            link(client, "I see.", 255)
            pic(client, 3)
            create(client)

        else

            if hasMagic(client, 1350, -1) then

                text(client, "You have learned this spell.")
                link(client, "I see.", 255)
                pic(client, 3)
                create(client)

            else

                awardMagic(client, 1350, 0)
                text(client, "You have learned Divine Hare.")
                link(client, "I see.", 255)
                pic(client, 3)
                create(client)
                deleteItem(self)

            end

        end

    else

        if getProfession(client) == 133 then

            if getLevel(client) < 54 then

                text(client, "Only Water Taoists can learn this spell after they reach level 54.")
                link(client, "I see.", 255)
                pic(client, 3)
                create(client)

            else

                if hasMagic(client, 1350, -1) then

                    text(client, "You have learned this spell.")
                    link(client, "I see.", 255)
                    pic(client, 3)
                    create(client)

                else

                    awardMagic(client, 1350, 0)
                    text(client, "You have learned Divine Hare.")
                    link(client, "I see.", 255)
                    pic(client, 3)
                    create(client)
                    deleteItem(self)

                end

            end

        else

            if getProfession(client) == 134 then

                if getLevel(client) < 54 then

                    text(client, "Only Water Taoists can learn this spell after they reach level 54.")
                    link(client, "I see.", 255)
                    pic(client, 3)
                    create(client)

                else

                    if hasMagic(client, 1350, -1) then

                        text(client, "You have learned this spell.")
                        link(client, "I see.", 255)
                        pic(client, 3)
                        create(client)

                    else

                        awardMagic(client, 1350, 0)
                        text(client, "You have learned Divine Hare.")
                        link(client, "I see.", 255)
                        pic(client, 3)
                        create(client)
                        deleteItem(self)

                    end

                end

            else

                if getProfession(client) == 135 then

                    if getLevel(client) < 54 then

                        text(client, "Only Water Taoists can learn this spell after they reach level 54.")
                        link(client, "I see.", 255)
                        pic(client, 3)
                        create(client)

                    else

                        if hasMagic(client, 1350, -1) then

                            text(client, "You have learned this spell.")
                            link(client, "I see.", 255)
                            pic(client, 3)
                            create(client)

                        else

                            awardMagic(client, 1350, 0)
                            text(client, "You have learned Divine Hare.")
                            link(client, "I see.", 255)
                            pic(client, 3)
                            create(client)
                            deleteItem(self)

                        end

                    end

                else

                    text(client, "Only Water Taoists can learn this spell after they reach level 54.")
                    link(client, "I see.", 255)
                    pic(client, 3)
                    create(client)

                end

            end

        end

    end

end
