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

function processTask30164(client, idx)
    name = "Mike"
    face = 1

    if (idx == 0) then

        if hasItem(client, 721220, 1) then

            text(client, "Joy is a good girl.")
            link(client, "I think so.", 255)
            pic(client, 72)
            create(client)

        else

            if hasItem(client, 721221, 1) then

                text(client, "I am very pleased to help Joy, but I need some raw material. Can you give me?")
                link(client, "What material do you need?", 1)
                link(client, "Sorry, I am helpless.", 255)
                pic(client, 72)
                create(client)

            else

                text(client, "Please do not disturb me while I am working.")
                link(client, "Good Bye.", 255)
                pic(client, 72)
                create(client)

            end

        end

    elseif (idx == 1) then

        text(client, "I need two normal Fury Gems, one normal Dragon Gem and 5 Euxenite Ores.")
        link(client, "Sorry, I am helpless.", 255)
        link(client, "Here you are.", 2)
        pic(client, 72)
        create(client)

    elseif (idx == 2) then

        if hasItem(client, 700021, 2) then

            if hasItem(client, 700011, 1) then

                if hasItem(client, 1072031, 5) then

                    spendItem(client, 721221, 1)
                    if spendItem(client, 700021, 2) then

                        if spendItem(client, 700011, 1) then

                            if spendItem(client, 1072031, 5) then

                                awardItem(client, "721220", 1)
                                text(client, "The magic bow is made. You may give it to Joy now.")
                                link(client, "Thanks.", 255)
                                pic(client, 72)
                                create(client)

                            else

                                text(client, "Sorry, you do not have 5 Euxenite Ores.")
                                link(client, "I see.", 255)
                                pic(client, 72)
                                create(client)

                            end

                        else

                            text(client, "Sorry, you do not have a normal Dragon Gem.")
                            link(client, "I see.", 255)
                            pic(client, 72)
                            create(client)

                        end

                    else

                        text(client, "Sorry, you do not have two normal Fury Gems.")
                        link(client, "I see.", 255)
                        pic(client, 72)
                        create(client)

                    end

                else

                    text(client, "Sorry, you do not have 5 Euxenite Ores.")
                    link(client, "I see.", 255)
                    pic(client, 72)
                    create(client)

                end

            else

                text(client, "Sorry, you do not have a normal Dragon Gem.")
                link(client, "I see.", 255)
                pic(client, 72)
                create(client)

            end

        else

            text(client, "Sorry, you do not have two normal Fury Gems.")
            link(client, "I see.", 255)
            pic(client, 72)
            create(client)

        end

    end

end
