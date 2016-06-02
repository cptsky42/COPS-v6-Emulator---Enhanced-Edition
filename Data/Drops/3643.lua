--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/10/2015 3:10:24 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function Monster3643_OnDie(self, client)
    name = "Theodore"

    getUserStats(client, 61, 0) == 2
    if getUserStats(client, 61, 2) == 7 then

        if getUserStats(client, 61, 21) > 7 then

            getUserStats(client, 61, 21) == 8
            setUserStats(client, 61, 2, 0, true)
            setUserStats(client, 61, 21, getUserStats(61, 21) + 1, true)
            sendSysMsg(client, "The BindingCurse has been broken into pieces. It has shed all of its blood, but it is still possessed by the evil force.", 2011)

        else

            sendSysMsg(client, "You`ve broken 8 curses. The BindingCurse has shed a lot of its blood. The evil force has temporarily disappeared.", 2011)
            setUserStats(client, 61, 2, 0, true)
            setUserStats(client, 61, 21, getUserStats(61, 21) + 1, true)

        end

    else

        if getUserStats(client, 61, 21) == 9 then


        else

            if getUserStats(client, 61, 2) == 0 then

                sendSysMsg(client, "The BindingCurse has been weakened, but there are no signs of fading away. It seems that you should go to kill Andrew.", 2011)

            else

                if getUserStats(client, 61, 2) == 1 then

                    sendSysMsg(client, "The BindingCurse has been weakened, but there are no signs of fading away. It seems that you should go to kill Peter.", 2011)

                else

                    if getUserStats(client, 61, 2) == 2 then

                        sendSysMsg(client, "The BindingCurse has been weakened, but there are no signs of fading away. It seems that you should go to kill Philip.", 2011)

                    else

                        if getUserStats(client, 61, 2) == 3 then

                            sendSysMsg(client, "The BindingCurse has been weakened, but there are no signs of fading away. It seems that you should go to kill Timothy.", 2011)

                        else

                            if getUserStats(client, 61, 2) == 4 then

                                sendSysMsg(client, "The BindingCurse has been weakened, but there are no signs of fading away. It seems that you should go to kill Daphne.", 2011)

                            else

                                if getUserStats(client, 61, 2) == 5 then

                                    sendSysMsg(client, "The BindingCurse has been weakened, but there are no signs of fading away. It seems that you should go to kill Victoria.", 2011)

                                else

                                    getUserStats(client, 61, 2) == 6
                                    sendSysMsg(client, "The BindingCurse has been weakened, but there are no signs of fading away. It seems that you should go to kill Wayne.", 2011)

                                end

                            end

                        end

                    end

                end

            end

        end

    end

end
