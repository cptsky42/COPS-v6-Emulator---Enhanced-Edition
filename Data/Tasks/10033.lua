--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:42 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask10033(client, idx)
    name = "SpaceMark"
    face = 1

    if (idx == 0) then

        if getProfession(client) == 132 then

            text(client, "If you give me 100 silver, I can make a teleport scroll for you. You can use it to return here directly.")
            link(client, "Please make one for me.", 1)
            link(client, "Just passing by.", 255)
            pic(client, 1)
            create(client)

        else

            if getProfession(client) == 133 then

                text(client, "If you give me 100 silver, I can make a teleport scroll for you. You can use it to return here directly.")
                link(client, "Please make one for me.", 1)
                link(client, "Just passing by.", 255)
                pic(client, 1)
                create(client)

            else

                if getProfession(client) == 134 then

                    text(client, "If you give me 100 silver, I can make a teleport scroll for you. You can use it to return here directly.")
                    link(client, "Please make one for me.", 1)
                    link(client, "Just passing by.", 255)
                    pic(client, 1)
                    create(client)

                else

                    if getProfession(client) == 135 then

                        text(client, "If you give me 100 silver, I can make a teleport scroll for you. You can use it to return here directly.")
                        link(client, "Please make one for me.", 1)
                        link(client, "Just passing by.", 255)
                        pic(client, 1)
                        create(client)

                    else

                        text(client, "Only Water class can buy this teleport scroll.")
                        link(client, "I see.", 255)
                        pic(client, 1)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 1) then

        if spendMoney(client, 100) then

            awardItem(client, "1060027", 1)
            text(client, "After you right click on this scroll to use, you will be teleported here from any place.")
            link(client, "I see.", 255)
            pic(client, 1)
            create(client)

        else

            text(client, "Sorry, you do not have 100 silver.")
            link(client, "I see.", 255)
            pic(client, 1)
            create(client)

        end

    end

end
