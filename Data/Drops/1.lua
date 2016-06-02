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

function Monster1_OnDie(self, client)
    name = "Pheasant"

    if getProfession(client) <= 11 then

        if getMetempsychosis(client) < 1 then

            if getLevel(client) < 5 then

                if (rand(client, 20) < 3) then

                    if (rand(client, 15) < 1) then

                        dropItem(self, client, 410004)

                    else

                        if (rand(client, 14) < 1) then

                            dropItem(self, client, 420004)

                        else

                            if (rand(client, 13) < 1) then

                                dropItem(self, client, 430004)

                            else

                                if (rand(client, 12) < 1) then

                                    dropItem(self, client, 440004)

                                else

                                    if (rand(client, 11) < 1) then

                                        dropItem(self, client, 450004)

                                    else

                                        if (rand(client, 10) < 1) then

                                            dropItem(self, client, 460004)

                                        else

                                            if (rand(client, 9) < 1) then

                                                dropItem(self, client, 480004)

                                            else

                                                if (rand(client, 8) < 1) then

                                                    dropItem(self, client, 481004)

                                                else

                                                    if (rand(client, 7) < 1) then

                                                        dropItem(self, client, 490004)

                                                    else

                                                        if (rand(client, 6) < 1) then

                                                            dropItem(self, client, 510004)

                                                        else

                                                            if (rand(client, 5) < 1) then

                                                                dropItem(self, client, 530004)

                                                            else

                                                                if (rand(client, 4) < 1) then

                                                                    dropItem(self, client, 540004)

                                                                else

                                                                    if (rand(client, 3) < 1) then

                                                                        dropItem(self, client, 560004)

                                                                    else

                                                                        if (rand(client, 2) < 1) then

                                                                            dropItem(self, client, 580004)

                                                                        else

                                                                            dropItem(self, client, 561004)

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

                    if (rand(client, 20) < 1) then

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

                    else

                        if (rand(client, 100) < 1) then

                            dropItem(self, client, 710001)
                            
                        end

                    end

                end

            else

                if (rand(client, 100) < 1) then

                    dropItem(self, client, 710001)

                end

            end

        else

            if (rand(client, 100) < 1) then

                dropItem(self, client, 710001)

            end

        end

    else

        if getProfession(client) <= 21 then

            if getMetempsychosis(client) < 1 then

                if getLevel(client) < 5 then

                    if (rand(client, 20) < 3) then

                        if (rand(client, 15) < 1) then

                            dropItem(self, client, 410004)

                        else

                            if (rand(client, 14) < 1) then

                                dropItem(self, client, 420004)

                            else

                                if (rand(client, 13) < 1) then

                                    dropItem(self, client, 430004)

                                else

                                    if (rand(client, 12) < 1) then

                                        dropItem(self, client, 440004)

                                    else

                                        if (rand(client, 11) < 1) then

                                            dropItem(self, client, 450004)

                                        else

                                            if (rand(client, 10) < 1) then

                                                dropItem(self, client, 460004)

                                            else

                                                if (rand(client, 9) < 1) then

                                                    dropItem(self, client, 480004)

                                                else

                                                    if (rand(client, 8) < 1) then

                                                        dropItem(self, client, 481004)

                                                    else

                                                        if (rand(client, 7) < 1) then

                                                            dropItem(self, client, 490004)

                                                        else

                                                            if (rand(client, 6) < 1) then

                                                                dropItem(self, client, 510004)

                                                            else

                                                                if (rand(client, 5) < 1) then

                                                                    dropItem(self, client, 530004)

                                                                else

                                                                    if (rand(client, 4) < 1) then

                                                                        dropItem(self, client, 540004)

                                                                    else

                                                                        if (rand(client, 3) < 1) then

                                                                            dropItem(self, client, 560004)

                                                                        else

                                                                            if (rand(client, 2) < 1) then

                                                                                dropItem(self, client, 580004)

                                                                            else

                                                                                dropItem(self, client, 561004)

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

                        if (rand(client, 20) < 1) then

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

                        else

                            if (rand(client, 100) < 1) then

                                dropItem(self, client, 710001)

                            end

                        end

                    end

                else

                    if (rand(client, 100) < 1) then

                        dropItem(self, client, 710001)

                    end

                end

            else

                if (rand(client, 100) < 1) then

                    dropItem(self, client, 710001)

                end

            end

        else

            if (rand(client, 100) < 1) then

                dropItem(self, client, 710001)

            end

        end

    end

end
