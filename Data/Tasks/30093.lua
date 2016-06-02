--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:49 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30093(client, idx)
    name = "ProtectingKid"
    face = 1

    if (idx == 0) then

        if hasTaskItem(client, "CloudLock") and (getMoney(client) >= 0) then

            text(client, "You don`t take Lock with you and I will send you back to Maze.")
            link(client, "Alright.", 1)
            pic(client, 10)
            create(client)

        elseif hasTaskItem(client, "DragonLock") and (getMoney(client) >= 0) then

            text(client, "You don`t take Lock and I will send you back to Maze.")
            link(client, "Alright.", 2)
            pic(client, 10)
            create(client)

        elseif hasTaskItem(client, "PhoenixLock") and (getMoney(client) >= 0) then

            text(client, "Well done. Only I can teleport you to claim the prize, that guy standing by me will teleport you back. Do you trust me?")
            link(client, "Yes, I do.", 3)
            link(client, "You are playing tricks.", 255)
            pic(client, 10)
            create(client)

        else

         text(client, "Since you do not bring the maze lock, I have to teleport you out of the maze. I hope you can get the big prize next time.")
         link(client, "I hope so.", 4)
         pic(client, 10)
         create(client)

        end

    elseif (idx == 1) then

        if hasTaskItem(client, "CloudLock") and (getMoney(client) >= 0) then

            if hasItem(client, 721110, 1) then

                if (rand(client, 2) < 1) then

                    action = randomAction(client, 1, 8)
                    if action == 1 then
                        move(client, 1061, 318, 393)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 2 then
                        move(client, 1061, 360, 418)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 3 then
                        move(client, 1061, 310, 381)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 4 then
                        move(client, 1061, 311, 432)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 5 then
                        move(client, 1061, 283, 342)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 6 then
                        move(client, 1061, 283, 356)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 7 then
                        move(client, 1061, 242, 376)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 8 then
                        move(client, 1061, 259, 355)
                        setRecordPos(client, 1020, 566, 565)
                    end


                else

                    action = randomAction(client, 1, 8)
                    if action == 1 then
                        move(client, 1062, 318, 393)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 2 then
                        move(client, 1062, 360, 418)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 3 then
                        move(client, 1062, 360, 418)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 4 then
                        move(client, 1062, 310, 381)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 5 then
                        move(client, 1062, 311, 432)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 6 then
                        move(client, 1062, 283, 342)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 7 then
                        move(client, 1062, 283, 356)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 8 then
                        move(client, 1062, 242, 376)
                        setRecordPos(client, 1020, 566, 565)
                    end


                end

            else

                if (rand(client, 2) < 1) then

                    action = randomAction(client, 1, 8)
                    if action == 1 then
                        move(client, 1060, 318, 393)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 2 then
                        move(client, 1060, 360, 418)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 3 then
                        move(client, 1060, 310, 381)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 4 then
                        move(client, 1060, 311, 432)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 5 then
                        move(client, 1060, 283, 342)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 6 then
                        move(client, 1060, 283, 356)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 7 then
                        move(client, 1060, 242, 376)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 8 then
                        move(client, 1060, 259, 355)
                        setRecordPos(client, 1020, 566, 565)
                    end


                else

                    action = randomAction(client, 1, 8)
                    if action == 1 then
                        move(client, 1062, 318, 393)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 2 then
                        move(client, 1062, 360, 418)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 3 then
                        move(client, 1062, 360, 418)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 4 then
                        move(client, 1062, 310, 381)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 5 then
                        move(client, 1062, 311, 432)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 6 then
                        move(client, 1062, 283, 342)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 7 then
                        move(client, 1062, 283, 356)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 8 then
                        move(client, 1062, 242, 376)
                        setRecordPos(client, 1020, 566, 565)
                    end


                end

            end

        end

    elseif (idx == 2) then

        if hasTaskItem(client, "DragonLock") and (getMoney(client) >= 0) then

            if hasItem(client, 721112, 1) then

                if (rand(client, 2) < 1) then

                    action = randomAction(client, 1, 8)
                    if action == 1 then
                        move(client, 1060, 318, 393)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 2 then
                        move(client, 1060, 360, 418)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 3 then
                        move(client, 1060, 310, 381)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 4 then
                        move(client, 1060, 311, 432)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 5 then
                        move(client, 1060, 283, 342)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 6 then
                        move(client, 1060, 283, 356)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 7 then
                        move(client, 1060, 242, 376)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 8 then
                        move(client, 1060, 259, 355)
                        setRecordPos(client, 1020, 566, 565)
                    end


                else

                    action = randomAction(client, 1, 8)
                    if action == 1 then
                        move(client, 1061, 318, 393)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 2 then
                        move(client, 1061, 360, 418)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 3 then
                        move(client, 1061, 310, 381)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 4 then
                        move(client, 1061, 311, 432)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 5 then
                        move(client, 1061, 283, 342)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 6 then
                        move(client, 1061, 283, 356)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 7 then
                        move(client, 1061, 242, 376)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 8 then
                        move(client, 1061, 259, 355)
                        setRecordPos(client, 1020, 566, 565)
                    end


                end

            else

                if (rand(client, 2) < 1) then

                    action = randomAction(client, 1, 8)
                    if action == 1 then
                        move(client, 1061, 318, 393)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 2 then
                        move(client, 1061, 360, 418)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 3 then
                        move(client, 1061, 310, 381)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 4 then
                        move(client, 1061, 311, 432)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 5 then
                        move(client, 1061, 283, 342)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 6 then
                        move(client, 1061, 283, 356)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 7 then
                        move(client, 1061, 242, 376)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 8 then
                        move(client, 1061, 259, 355)
                        setRecordPos(client, 1020, 566, 565)
                    end


                else

                    action = randomAction(client, 1, 8)
                    if action == 1 then
                        move(client, 1062, 318, 393)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 2 then
                        move(client, 1062, 360, 418)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 3 then
                        move(client, 1062, 360, 418)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 4 then
                        move(client, 1062, 310, 381)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 5 then
                        move(client, 1062, 311, 432)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 6 then
                        move(client, 1062, 283, 342)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 7 then
                        move(client, 1062, 283, 356)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 8 then
                        move(client, 1062, 242, 376)
                        setRecordPos(client, 1020, 566, 565)
                    end


                end

            end

        end

    elseif (idx == 3) then

        if hasTaskItem(client, "PhoenixLock") and (getMoney(client) >= 0) then

            if (rand(client, 2) < 1) then

                moveNpc(client, 30093, 1062, 510, 324)
                moveNpc(client, 30073, 1062, 512, 322)
                if (rand(client, 2) < 1) then

                    action = randomAction(client, 1, 8)
                    if action == 1 then
                        move(client, 1060, 318, 393)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 2 then
                        move(client, 1060, 360, 418)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 3 then
                        move(client, 1060, 310, 381)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 4 then
                        move(client, 1060, 311, 432)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 5 then
                        move(client, 1060, 283, 342)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 6 then
                        move(client, 1060, 283, 356)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 7 then
                        move(client, 1060, 242, 376)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 8 then
                        move(client, 1060, 259, 355)
                        setRecordPos(client, 1020, 566, 565)
                    end


                else

                    action = randomAction(client, 1, 8)
                    if action == 1 then
                        move(client, 1061, 318, 393)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 2 then
                        move(client, 1061, 360, 418)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 3 then
                        move(client, 1061, 310, 381)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 4 then
                        move(client, 1061, 311, 432)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 5 then
                        move(client, 1061, 283, 342)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 6 then
                        move(client, 1061, 283, 356)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 7 then
                        move(client, 1061, 242, 376)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 8 then
                        move(client, 1061, 259, 355)
                        setRecordPos(client, 1020, 566, 565)
                    end


                end

            else

                if (rand(client, 2) < 1) then

                    action = randomAction(client, 1, 8)
                    if action == 1 then
                        move(client, 1060, 318, 393)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 2 then
                        move(client, 1060, 360, 418)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 3 then
                        move(client, 1060, 310, 381)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 4 then
                        move(client, 1060, 311, 432)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 5 then
                        move(client, 1060, 283, 342)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 6 then
                        move(client, 1060, 283, 356)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 7 then
                        move(client, 1060, 242, 376)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 8 then
                        move(client, 1060, 259, 355)
                        setRecordPos(client, 1020, 566, 565)
                    end


                else

                    action = randomAction(client, 1, 8)
                    if action == 1 then
                        move(client, 1061, 318, 393)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 2 then
                        move(client, 1061, 360, 418)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 3 then
                        move(client, 1061, 310, 381)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 4 then
                        move(client, 1061, 311, 432)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 5 then
                        move(client, 1061, 283, 342)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 6 then
                        move(client, 1061, 283, 356)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 7 then
                        move(client, 1061, 242, 376)
                        setRecordPos(client, 1020, 566, 565)
                    elseif action == 8 then
                        move(client, 1061, 259, 355)
                        setRecordPos(client, 1020, 566, 565)
                    end


                end

            end

        end

    elseif (idx == 4) then

        move(client, 1020, 566, 565)

    end

end
