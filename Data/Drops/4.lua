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

function Monster4_OnDie(self, client)
    name = "Apparition"

    if getProfession(client) <= 11 then

        if getMetempsychosis(client) < 1 then

            if getLevel(client) < 20 then

                if (rand(client, 100) < 1) then

                    if (rand(client, 15) < 1) then

                        dropItem(self, client, 410034)

                    else

                        if (rand(client, 14) < 1) then

                            dropItem(self, client, 420034)

                        else

                            if (rand(client, 13) < 1) then

                                dropItem(self, client, 430034)

                            else

                                if (rand(client, 12) < 1) then

                                    dropItem(self, client, 440034)

                                else

                                    if (rand(client, 11) < 1) then

                                        dropItem(self, client, 450034)

                                    else

                                        if (rand(client, 10) < 1) then

                                            dropItem(self, client, 460034)

                                        else

                                            if (rand(client, 9) < 1) then

                                                dropItem(self, client, 480034)

                                            else

                                                if (rand(client, 8) < 1) then

                                                    dropItem(self, client, 481034)

                                                else

                                                    if (rand(client, 7) < 1) then

                                                        dropItem(self, client, 490034)

                                                    else

                                                        if (rand(client, 6) < 1) then

                                                            dropItem(self, client, 510034)

                                                        else

                                                            if (rand(client, 5) < 1) then

                                                                dropItem(self, client, 530034)

                                                            else

                                                                if (rand(client, 4) < 1) then

                                                                    dropItem(self, client, 540034)

                                                                else

                                                                    if (rand(client, 3) < 1) then

                                                                        dropItem(self, client, 560034)

                                                                    else

                                                                        if (rand(client, 2) < 1) then

                                                                            dropItem(self, client, 580034)

                                                                        else

                                                                            dropItem(self, client, 561034)

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

    else

        if getProfession(client) <= 21 then

            if getMetempsychosis(client) < 1 then

                if getLevel(client) < 20 then

                    if (rand(client, 100) < 1) then

                        if (rand(client, 15) < 1) then

                            dropItem(self, client, 410034)

                        else

                            if (rand(client, 14) < 1) then

                                dropItem(self, client, 420034)

                            else

                                if (rand(client, 13) < 1) then

                                    dropItem(self, client, 430034)

                                else

                                    if (rand(client, 12) < 1) then

                                        dropItem(self, client, 440034)

                                    else

                                        if (rand(client, 11) < 1) then

                                            dropItem(self, client, 450034)

                                        else

                                            if (rand(client, 10) < 1) then

                                                dropItem(self, client, 460034)

                                            else

                                                if (rand(client, 9) < 1) then

                                                    dropItem(self, client, 480034)

                                                else

                                                    if (rand(client, 8) < 1) then

                                                        dropItem(self, client, 481034)

                                                    else

                                                        if (rand(client, 7) < 1) then

                                                            dropItem(self, client, 490034)

                                                        else

                                                            if (rand(client, 6) < 1) then

                                                                dropItem(self, client, 510034)

                                                            else

                                                                if (rand(client, 5) < 1) then

                                                                    dropItem(self, client, 530034)

                                                                else

                                                                    if (rand(client, 4) < 1) then

                                                                        dropItem(self, client, 540034)

                                                                    else

                                                                        if (rand(client, 3) < 1) then

                                                                            dropItem(self, client, 560034)

                                                                        else

                                                                            if (rand(client, 2) < 1) then

                                                                                dropItem(self, client, 580034)

                                                                            else

                                                                                dropItem(self, client, 561034)

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

    end

end
