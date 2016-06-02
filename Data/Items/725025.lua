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

function useItem725025(self, client)
    name = "FlyingMoon"
    face = 1

    if getLevel(client) < 40 then

        text(client, "Sorry, only Warriors can learn this skill after they are level 40.")
        link(client, "I see.", 255)
        pic(client, 3)
        create(client)

    else

        if getProfession(client) == 22 then

            if hasMagic(client, 1320, -1) then

                text(client, "You have learned this spell.")
                link(client, "I see.", 255)
                pic(client, 3)
                create(client)

            else

                awardMagic(client, 1320, 0)
                text(client, "You have learned Flying Moon.")
                link(client, "I see.", 255)
                pic(client, 3)
                create(client)
                deleteItem(self)

            end

        else

            if getProfession(client) == 23 then

                if hasMagic(client, 1320, -1) then

                    text(client, "You have learned this spell.")
                    link(client, "I see.", 255)
                    pic(client, 3)
                    create(client)

                else

                    awardMagic(client, 1320, 0)
                    text(client, "You have learned Flying Moon.")
                    link(client, "I see.", 255)
                    pic(client, 3)
                    create(client)
                    deleteItem(self)

                end

            else

                if getProfession(client) == 24 then

                    if hasMagic(client, 1320, -1) then

                        text(client, "You have learned this spell.")
                        link(client, "I see.", 255)
                        pic(client, 3)
                        create(client)

                    else

                        awardMagic(client, 1320, 0)
                        text(client, "You have learned Flying Moon.")
                        link(client, "I see.", 255)
                        pic(client, 3)
                        create(client)
                        deleteItem(self)

                    end

                else

                    if getProfession(client) == 25 then

                        if hasMagic(client, 1320, -1) then

                            text(client, "You have learned this spell.")
                            link(client, "I see.", 255)
                            pic(client, 3)
                            create(client)

                        else

                            awardMagic(client, 1320, 0)
                            text(client, "You have learned Flying Moon.")
                            link(client, "I see.", 255)
                            pic(client, 3)
                            create(client)
                            deleteItem(self)

                        end

                    else

                        text(client, "Sorry, only Warriors can learn this skill after they are level 40.")
                        link(client, "I see.", 255)
                        pic(client, 3)
                        create(client)

                    end

                end

            end

        end

    end

end
