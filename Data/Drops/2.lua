--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/10/2015 3:10:14 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function Monster2_OnDie(self, client)
    name = "Turtledove"

    if getProfession(client) <= 11 then

        if getMetempsychosis(client) < 1 and getLevel(client) < 10 then
        
            if (rand(client, 25) < 1) then

                if (rand(client, 15) < 1) then

                    dropItem(self, client, 410014)

                else

                    if (rand(client, 14) < 1) then

                        dropItem(self, client, 420014)

                    else

                        if (rand(client, 13) < 1) then

                            dropItem(self, client, 430014)

                        else

                            if (rand(client, 12) < 1) then

                                dropItem(self, client, 440014)

                            else

                                if (rand(client, 11) < 1) then

                                    dropItem(self, client, 450014)

                                else

                                    if (rand(client, 10) < 1) then

                                        dropItem(self, client, 460014)

                                    else

                                        if (rand(client, 9) < 1) then

                                            dropItem(self, client, 480014)

                                        else

                                            if (rand(client, 8) < 1) then

                                                dropItem(self, client, 481014)

                                            else

                                                if (rand(client, 7) < 1) then

                                                    dropItem(self, client, 490014)

                                                else

                                                    if (rand(client, 6) < 1) then

                                                        dropItem(self, client, 510014)

                                                    else

                                                        if (rand(client, 5) < 1) then

                                                            dropItem(self, client, 530014)

                                                        else

                                                            if (rand(client, 4) < 1) then

                                                                dropItem(self, client, 540014)

                                                            else

                                                                if (rand(client, 3) < 1) then

                                                                    dropItem(self, client, 560014)

                                                                else

                                                                    if (rand(client, 2) < 1) then

                                                                        dropItem(self, client, 580014)

                                                                    else

                                                                        dropItem(self, client, 561014)

                                                                    end

                                                                end

                                                            end

                                                        end

                                                    end

                                                end

                                            end

                                        end

                                    end

                                end

                            end

                        end

                    end

                end

            else

                if (rand(client, 50) < 1) then

                    if (rand(client, 14) < 1) then

                        dropItem(self, client, 721287)
                        sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                    else

                        if (rand(client, 13) < 1) then

                            dropItem(self, client, 721288)
                            sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                        else

                            if (rand(client, 12) < 1) then

                                dropItem(self, client, 721273)
                                sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                            else

                                if (rand(client, 11) < 1) then

                                    dropItem(self, client, 721274)
                                    sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                else

                                    if (rand(client, 10) < 1) then

                                        dropItem(self, client, 721275)
                                        sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                    else

                                        if (rand(client, 9) < 1) then

                                            dropItem(self, client, 721276)
                                            sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                        else

                                            if (rand(client, 8) < 1) then

                                                dropItem(self, client, 721277)
                                                sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                            else

                                                if (rand(client, 7) < 1) then

                                                    dropItem(self, client, 721278)
                                                    sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                                else

                                                    if (rand(client, 6) < 1) then

                                                        dropItem(self, client, 721279)
                                                        sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                                    else

                                                        if (rand(client, 5) < 1) then

                                                            dropItem(self, client, 721280)
                                                            sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                                        else

                                                            if (rand(client, 4) < 1) then

                                                                dropItem(self, client, 721281)
                                                                sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                                            else

                                                                if (rand(client, 3) < 1) then

                                                                    dropItem(self, client, 721282)
                                                                    sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                                                else

                                                                    if (rand(client, 2) < 1) then

                                                                        dropItem(self, client, 721283)
                                                                        sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                                                    else

                                                                        dropItem(self, client, 721284)
                                                                        sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                                                    end

                                                                end

                                                            end

                                                        end

                                                    end

                                                end

                                            end

                                        end

                                    end

                                end

                            end

                        end

                    end

                end

            end

        end

    else

        if getProfession(client) <= 21 and getMetempsychosis(client) < 1 and getLevel(client) < 10 then

            if (rand(client, 25) < 1) then

                if (rand(client, 15) < 1) then

                    dropItem(self, client, 410014)

                else

                    if (rand(client, 14) < 1) then

                        dropItem(self, client, 420014)

                    else

                        if (rand(client, 13) < 1) then

                            dropItem(self, client, 430014)

                        else

                            if (rand(client, 12) < 1) then

                                dropItem(self, client, 440014)

                            else

                                if (rand(client, 11) < 1) then

                                    dropItem(self, client, 450014)

                                else

                                    if (rand(client, 10) < 1) then

                                        dropItem(self, client, 460014)

                                    else

                                        if (rand(client, 9) < 1) then

                                            dropItem(self, client, 480014)

                                        else

                                            if (rand(client, 8) < 1) then

                                                dropItem(self, client, 481014)

                                            else

                                                if (rand(client, 7) < 1) then

                                                    dropItem(self, client, 490014)

                                                else

                                                    if (rand(client, 6) < 1) then

                                                        dropItem(self, client, 510014)

                                                    else

                                                        if (rand(client, 5) < 1) then

                                                            dropItem(self, client, 530014)

                                                        else

                                                            if (rand(client, 4) < 1) then

                                                                dropItem(self, client, 540014)

                                                            else

                                                                if (rand(client, 3) < 1) then

                                                                    dropItem(self, client, 560014)

                                                                else

                                                                    if (rand(client, 2) < 1) then

                                                                        dropItem(self, client, 580014)

                                                                    else

                                                                        dropItem(self, client, 561014)

                                                                    end

                                                                end

                                                            end

                                                        end

                                                    end

                                                end

                                            end

                                        end

                                    end

                                end

                            end

                        end

                    end

                end

            else

                if (rand(client, 50) < 1) then

                    if (rand(client, 14) < 1) then

                        dropItem(self, client, 721287)
                        sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                    else

                        if (rand(client, 13) < 1) then

                            dropItem(self, client, 721288)
                            sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                        else

                            if (rand(client, 12) < 1) then

                                dropItem(self, client, 721273)
                                sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                            else

                                if (rand(client, 11) < 1) then

                                    dropItem(self, client, 721274)
                                    sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                else

                                    if (rand(client, 10) < 1) then

                                        dropItem(self, client, 721275)
                                        sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                    else

                                        if (rand(client, 9) < 1) then

                                            dropItem(self, client, 721276)
                                            sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                        else

                                            if (rand(client, 8) < 1) then

                                                dropItem(self, client, 721277)
                                                sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                            else

                                                if (rand(client, 7) < 1) then

                                                    dropItem(self, client, 721278)
                                                    sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                                else

                                                    if (rand(client, 6) < 1) then

                                                        dropItem(self, client, 721279)
                                                        sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                                    else

                                                        if (rand(client, 5) < 1) then

                                                            dropItem(self, client, 721280)
                                                            sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                                        else

                                                            if (rand(client, 4) < 1) then

                                                                dropItem(self, client, 721281)
                                                                sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                                            else

                                                                if (rand(client, 3) < 1) then

                                                                    dropItem(self, client, 721282)
                                                                    sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                                                else

                                                                    if (rand(client, 2) < 1) then

                                                                        dropItem(self, client, 721283)
                                                                        sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                                                    else

                                                                        dropItem(self, client, 721284)
                                                                        sendSysMsg(client, "With a flash of light a weapon skill book comes out of the monster you`ve killed!", 2005)

                                                                    end

                                                                end

                                                            end

                                                        end

                                                    end

                                                end

                                            end

                                        end

                                    end

                                end

                            end

                        end

                    end

                end

            end

        end

    end

end
