--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:28 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask3610(client, idx)
    name = "Carl"
    face = 1

    if (idx == 0) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "God has sent me here to collect a WhiteFlower and")
            text(client, "RedFlower, but I don`t know where to get them. I only know")
            text(client, "that they are made of 6 petals,a Pistil and Stalk. If you")
            text(client, "can find them for me, I will be very grateful to you and")
            text(client, "reward you.")
            text(client, "")
            link(client, "I`ve got to go.", 255)
            link(client, "I`ve just got them.", 1)
            pic(client, 62)
            create(client)

        end

    elseif (idx == 1) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if hasItem(client, 722732, 1) then

                if hasItem(client, 722736, 1) then

                    text(client, "Yeah, they are the right flowers I`m looking for. Thank you very much. I give you my token. You may exchange it for")
                    text(client, "some good prizes in the future. Please keep it well.")
                    text(client, "")
                    link(client, "Thanks a lot.", 2)
                    pic(client, 62)
                    create(client)

                else

                    if hasItem(client, 722732, 1) then

                        text(client, "Sorry, there is no WhiteFlower in your inventory. Don`t try to cheat me.")
                        link(client, "Sorry, I forgot to bring.", 255)
                        pic(client, 62)
                        create(client)

                    else

                        text(client, "Sorry, there is no RedFlower or WhiteFlower in your inventory. Don`t try to cheat me.")
                        link(client, "Sorry, I forgot to bring.", 255)
                        pic(client, 62)
                        create(client)

                    end

                end

            else

                if hasItem(client, 722736, 1) then

                    text(client, "Sorry, there is no RedFlower in your inventory. Don`t try to cheat me.")
                    link(client, "Sorry, I forgot to bring.", 255)
                    pic(client, 62)
                    create(client)

                else

                    text(client, "Sorry, there is no RedFlower or WhiteFlower in your inventory. Don`t try to cheat me.")
                    link(client, "Sorry, I forgot to bring.", 255)
                    pic(client, 62)
                    create(client)

                end

            end

        end

    elseif (idx == 2) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            spendItem(client, 722732, 1)
            spendItem(client, 722736, 1)
            awardItem(client, "722740", 1)
            sendSysMsg(client, "You`ve got a HatefulToken!", 2005)

        end

    end

end
