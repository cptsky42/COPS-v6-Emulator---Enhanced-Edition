--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:11 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask140(client, idx)
    name = "Warden"
    face = 1

    if (idx == 0) then

        text(client, "What is the matter? If there is nothing special, do not disturb me.")
        link(client, "Can you let me out?", 1)
        link(client, "Just passing by.", 255)
        pic(client, 9)
        create(client)

    elseif (idx == 1) then

        if checkTime(client, 3, "5 14:00 5 23:59") then

            if checkTime(client, 5, "0 5") then

                text(client, "It is time for general pardon. You may leave now.")
                link(client, "Thanks.", 2)
                link(client, "I shall stay here.", 255)
                pic(client, 9)
                create(client)

            else

                if checkTime(client, 5, "30 35") then

                    text(client, "It is time for general pardon. You may leave now.")
                    link(client, "Thanks.", 2)
                    link(client, "I shall stay here.", 255)
                    pic(client, 9)
                    create(client)

                else

                    text(client, "Calm down and stay here. Learn what peace and love is. I will let you out later.")
                    link(client, "I see.", 255)
                    pic(client, 9)
                    create(client)

                end

            end

        else

            if checkTime(client, 3, "6 00:00 6 19:59") then

                if checkTime(client, 5, "0 5") then

                    text(client, "It is time for general pardon. You may leave now.")
                    link(client, "Thanks.", 2)
                    link(client, "I shall stay here.", 255)
                    pic(client, 9)
                    create(client)

                else

                    if checkTime(client, 5, "30 35") then

                        text(client, "It is time for general pardon. You may leave now.")
                        link(client, "Thanks.", 2)
                        link(client, "I shall stay here.", 255)
                        pic(client, 9)
                        create(client)

                    else

                        text(client, "Calm down and stay here. Learn what peace and love is. I will let you out later.")
                        link(client, "I see.", 255)
                        pic(client, 9)
                        create(client)

                    end

                end

            else

                text(client, "It is time for general pardon. You may leave now.")
                link(client, "Thanks.", 2)
                link(client, "I shall stay here.", 255)
                pic(client, 9)
                create(client)

            end

        end

    elseif (idx == 2) then

        move(client, 1002, 430, 380)

    end

end
