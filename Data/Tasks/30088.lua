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

function processTask30088(client, idx)
    name = "OldmanTang"
    face = 1

    if (idx == 0) then

        if hasItem(client, 121066, 1) then

            text(client, "Thank you for saving my life. Now I can detoxicate myself.")
            link(client, "You are welcome.", 255)
            pic(client, 8)
            create(client)

        else

            if hasItem(client, 151056, 1) then

                text(client, "Thank you for saving my life. Now I can detoxicate myself.")
                link(client, "You are welcome.", 255)
                pic(client, 8)
                create(client)

            else

                if hasItem(client, 121067, 1) then

                    text(client, "Thank you for saving my life. Now I can detoxicate myself.")
                    link(client, "You are welcome.", 255)
                    pic(client, 8)
                    create(client)

                else

                    if hasItem(client, 151057, 1) then

                        text(client, "Thank you for saving my life. Now I can detoxicate myself.")
                        link(client, "You are welcome.", 255)
                        pic(client, 8)
                        create(client)

                    else

                        if hasItem(client, 121068, 1) then

                            text(client, "Thank you for saving my life. Now I can detoxicate myself.")
                            link(client, "You are welcome.", 255)
                            pic(client, 8)
                            create(client)

                        else

                            if hasItem(client, 151058, 1) then

                                text(client, "Thank you for saving my life. Now I can detoxicate myself.")
                                link(client, "You are welcome.", 255)
                                pic(client, 8)
                                create(client)

                            else

                                if hasItem(client, 721120, 1) then

                                    text(client, "With this rat fang, I can be saved. Please take it as a reward.")
                                    link(client, "Thanks a lot.", 1)
                                    pic(client, 8)
                                    create(client)

                                else

                                    text(client, "My prayer comes true in the end. I thought I would die in this untraversed area. Would you help me out?")
                                    link(client, "What can I do?", 2)
                                    link(client, "Just passing by.", 255)
                                    pic(client, 8)
                                    create(client)

                                end

                            end

                        end

                    end

                end

            end

        end

    elseif (idx == 1) then

        if hasTaskItem(client, "RatFang") and (getMoney(client) >= 0) then

            spendItem(client, 721120, 1)
            if (rand(client, 3) < 1) then

                if (rand(client, 2) < 1) then

                    if (rand(client, 2) < 1) then

                        if (rand(client, 1000) < 1) then

                            awardItem(client, "121067", 1)

                        else

                            if (rand(client, 2) < 1) then

                                awardItem(client, "121066", 1)

                            else

                                awardItem(client, "151056", 1)

                            end

                        end

                    else

                        if (rand(client, 1000) < 1) then

                            awardItem(client, "151057", 1)

                        else

                            if (rand(client, 2) < 1) then

                                awardItem(client, "121066", 1)

                            else

                                awardItem(client, "151056", 1)

                            end

                        end

                    end

                else

                    if (rand(client, 2) < 1) then

                        if (rand(client, 2000) < 1) then

                            awardItem(client, "121068", 1)

                        else

                            if (rand(client, 2) < 1) then

                                if (rand(client, 2) < 1) then

                                    awardItem(client, "121066", 1)

                                else

                                    awardItem(client, "151056", 1)

                                end

                            else

                                if (rand(client, 2) < 1) then

                                    if (rand(client, 1000) < 1) then

                                        awardItem(client, "121067", 1)

                                    else

                                        if (rand(client, 2) < 1) then

                                            awardItem(client, "121066", 1)

                                        else

                                            awardItem(client, "151056", 1)

                                        end

                                    end

                                else

                                    if (rand(client, 1000) < 1) then

                                        awardItem(client, "151057", 1)

                                    else

                                        if (rand(client, 2) < 1) then

                                            awardItem(client, "121066", 1)

                                        else

                                            awardItem(client, "151056", 1)

                                        end

                                    end

                                end

                            end

                        end

                    else

                        if (rand(client, 2000) < 1) then

                            awardItem(client, "151058", 1)

                        else

                            if (rand(client, 2) < 1) then

                                if (rand(client, 2) < 1) then

                                    awardItem(client, "121066", 1)

                                else

                                    awardItem(client, "151056", 1)

                                end

                            else

                                if (rand(client, 2) < 1) then

                                    if (rand(client, 1000) < 1) then

                                        awardItem(client, "121067", 1)

                                    else

                                        if (rand(client, 2) < 1) then

                                            awardItem(client, "121066", 1)

                                        else

                                            awardItem(client, "151056", 1)

                                        end

                                    end

                                else

                                    if (rand(client, 1000) < 1) then

                                        awardItem(client, "151057", 1)

                                    else

                                        if (rand(client, 2) < 1) then

                                            awardItem(client, "121066", 1)

                                        else

                                            awardItem(client, "151056", 1)

                                        end

                                    end

                                end

                            end

                        end

                    end

                end

            else

                if (rand(client, 2) < 1) then

                    awardItem(client, "121066", 1)

                else

                    awardItem(client, "151056", 1)

                end

            end

        end

    elseif (idx == 2) then

        text(client, "I was hurt by poisonous rat on my way home. I can only restrain my wound from deteriorating with antidote.")
        link(client, "How can I help you?", 3)
        link(client, "I got to go.", 255)
        pic(client, 8)
        create(client)

    elseif (idx == 3) then

        text(client, "I need a rat fang as an ingredient.")
        link(client, "I shall bring one soon.", 255)
        link(client, "It is too dangerous.", 255)
        pic(client, 8)
        create(client)

    end

end
