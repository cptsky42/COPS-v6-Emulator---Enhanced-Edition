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

function useItem725028(self, client)
    name = "SpeedLightning"
    face = 1

    if getLevel(client) < 70 then

        text(client, "Sorry, only Water or Fire Wizards can learn this spell after they are level 70.")
        link(client, "I see.", 255)
        pic(client, 3)
        create(client)

    else

        if getProfession(client) == 143 then

            if hasMagic(client, 5001, -1) then

                text(client, "You have learned this spell.")
                link(client, "I see.", 255)
                pic(client, 3)
                create(client)

            else

                awardMagic(client, 5001, 0)
                text(client, "You have learned Speed Lightning.")
                link(client, "I see.", 255)
                pic(client, 3)
                create(client)
                deleteItem(self)

            end

        else

            if getProfession(client) == 144 then

                if hasMagic(client, 5001, -1) then

                    text(client, "You have learned this spell.")
                    link(client, "I see.", 255)
                    pic(client, 3)
                    create(client)

                else

                    awardMagic(client, 5001, 0)
                    text(client, "You have learned Speed Lightning.")
                    link(client, "I see.", 255)
                    pic(client, 3)
                    create(client)
                    deleteItem(self)

                end

            else

                if getProfession(client) == 145 then

                    if hasMagic(client, 5001, -1) then

                        text(client, "You have learned this spell.")
                        link(client, "I see.", 255)
                        pic(client, 3)
                        create(client)

                    else

                        awardMagic(client, 5001, 0)
                        text(client, "You have learned Speed Lightning.")
                        link(client, "I see.", 255)
                        pic(client, 3)
                        create(client)
                        deleteItem(self)

                    end

                else

                    if getProfession(client) == 133 then

                        if hasMagic(client, 5001, -1) then

                            text(client, "You have learned this spell.")
                            link(client, "I see.", 255)
                            pic(client, 3)
                            create(client)

                        else

                            awardMagic(client, 5001, 0)
                            text(client, "You have learned Speed Lightning.")
                            link(client, "I see.", 255)
                            pic(client, 3)
                            create(client)
                            deleteItem(self)

                        end

                    else

                        if getProfession(client) == 134 then

                            if hasMagic(client, 5001, -1) then

                                text(client, "You have learned this spell.")
                                link(client, "I see.", 255)
                                pic(client, 3)
                                create(client)

                            else

                                awardMagic(client, 5001, 0)
                                text(client, "You have learned Speed Lightning.")
                                link(client, "I see.", 255)
                                pic(client, 3)
                                create(client)
                                deleteItem(self)

                            end

                        else

                            if getProfession(client) == 135 then

                                if hasMagic(client, 5001, -1) then

                                    text(client, "You have learned this spell.")
                                    link(client, "I see.", 255)
                                    pic(client, 3)
                                    create(client)

                                else

                                    awardMagic(client, 5001, 0)
                                    text(client, "You have learned Speed Lightning.")
                                    link(client, "I see.", 255)
                                    pic(client, 3)
                                    create(client)
                                    deleteItem(self)

                                end

                            else

                                text(client, "Sorry, only Water or Fire Wizards can learn this spell after they are level 70.")
                                link(client, "I see.", 255)
                                pic(client, 3)
                                create(client)

                            end

                        end

                    end

                end

            end

        end

    end

end
