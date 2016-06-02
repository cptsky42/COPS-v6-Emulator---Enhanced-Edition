--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:37 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask6002(client, idx)
    name = "Warden"
    face = 1

    if (idx == 0) then

        if getUserStats(client, 6, 2) == 1 then

            text(client, "Your were sent here to take a visit by the PrisonOffcer. Are you going to leave now?")
            link(client, "Yes, let me out please.", 1)
            link(client, "Let me think it about.", 255)
            pic(client, 57)
            create(client)

        else

            text(client, "Do you know it is jail that you are in now?")
            link(client, "Have I done something wrong?", 2)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 1) then

        text(client, "OK, good luck. And see you later.")
        link(client, "Thank you. Byebye.", 3)
        pic(client, 57)
        create(client)

    elseif (idx == 2) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "Certainly. If you use any kind of bot, edit the game in any way, or you scam in game, you will be sent here.")
            link(client, "I see. And how can I leave here?", 4)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 3) then

        move(client, 1002, 434, 380)
        setUserStats(client, 6, 2, 0, true)

    elseif (idx == 4) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            text(client, "You must pay two DragonBalls for your first time being caught as punishment, four DragonBalls for your second")
            text(client, "time, and ten DragonBalls for your third time.")
            text(client, "If you are caught botting more than three times, you will have no chance to get released.")
            text(client, "")
            link(client, "There is no alternative for me.", 5)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 5) then

        if (getMoney(client) >= 0) and (getPkPoints(client) >= 0 and getPkPoints(client) <= 1000) then

            if getUserStats(client, 6, 1) == 0 then

                text(client, "I will charge you two DragonBalls to set you free since it is your first time. I do hope this is also your last time.")
                link(client, "OK.", 6)
                pic(client, 2)
                create(client)

            else

                if getUserStats(client, 6, 1) == 1 then

                    text(client, "It`s your second time being caught botting. You must pay four DragonBalls to get released.")
                    link(client, "OK.", 7)
                    pic(client, 2)
                    create(client)

                else

                    if getUserStats(client, 6, 1) == 2 then

                        text(client, "It is your third time being caught botting. I will not let you out unless you hand in 10 DragonBalls.")
                        link(client, "OK.", 8)
                        pic(client, 2)
                        create(client)

                    else

                        if getUserStats(client, 6, 1) >= 3 then

                            text(client, "It is unforgivable you are caught botting more than three times. Since you cannot play the game fair, just stay here forever!")
                            text(client, "")
                            link(client, "Ah, it`s so regrettable.", 255)
                            pic(client, 2)
                            create(client)

                        end

                    end

                end

            end


        end

    elseif (idx == 6) then

        if hasItem(client, 1088000, 2) then

            text(client, "I will let you out this time. If you are caught again, you must pay 4 DragonBalls for your guilty. Don`t let me see you again!")
            link(client, "I will NEVER bot again!", 9)
            pic(client, 2)
            create(client)

        else

            text(client, "Sorry, you do not have two DragonBalls, so I cannot release you.")
            link(client, "OK, I see.", 255)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 7) then

        if hasItem(client, 1088000, 4) then

            text(client, "I will let you out this time. Remember if you are caught botting next time, you must pay 10 DragonBalls for your guilty.")
            link(client, "I will NEVER bot again!", 10)
            pic(client, 2)
            create(client)

        else

            text(client, "Sorry, you do not have four DragonBalls, so I cannot release you.")
            link(client, "OK, I see.", 255)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 8) then

        if hasItem(client, 1088000, 10) then

            text(client, "It`s your last chance! If you are caught botting next time, nobody can set you free and you will have to stay in the")
            text(client, "Botjail forever. My friend, let`s play the game fairly and keep far away from bots!")
            link(client, "I will NEVER bot again!", 11)
            pic(client, 2)
            create(client)

        else

            text(client, "Sorry, you do not have four DragonBalls, so I cannot release you.")
            link(client, "Ah, it`s so regrettable.", 255)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 9) then

        if spendItem(client, 1088000, 2) then


        else

            text(client, "Sorry, you do not have two DragonBalls, so I cannot release you.")
            link(client, "OK, I see.", 255)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 10) then

        if spendItem(client, 1088000, 4) then


        else

            text(client, "Sorry, you do not have four DragonBalls, so I cannot release you.")
            link(client, "OK, I see.", 255)
            pic(client, 2)
            create(client)

        end

    elseif (idx == 11) then

        if spendItem(client, 1088000, 10) then


        else

            text(client, "Sorry, you do not have four DragonBalls, so I cannot release you.")
            link(client, "Ah, it`s so regrettable.", 255)
            pic(client, 2)
            create(client)

        end

    end

end
