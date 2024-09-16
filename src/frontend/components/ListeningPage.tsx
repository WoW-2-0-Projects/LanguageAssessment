"use client";
import { useRef, useState, useEffect } from 'react';
import "./styles/_audioListening.css";
import { Button, Modal } from 'antd';
import { useAssessmentContext } from '@/context/assessment';

export default function ListeningPage() {
    const audioRef = useRef<HTMLAudioElement | null>(null);
    const [isPlaying, setIsPlaying] = useState(false);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [isVisible, setIsVisible] = useState(false);

    const context = useAssessmentContext();
    const assessment = context?.assessment;
    const setAssessment = context?.setAssessment;

    const handlePlay = () => {
        if (audioRef.current) {
            audioRef.current.play()
                .then(() => {
                    setIsPlaying(true);
                })
                .catch((error) => {
                    console.error("Error playing audio:", error);
                });
        }
    };
    const handleEnded = () => {
        setIsPlaying(false);
        setIsVisible(true);
        if (!setAssessment) return;
        setAssessment(prev => {
            const newState = Object.assign({}, prev);
            newState.passedWarning = true;
            return newState;
        })
    };
    useEffect(() => {
        setIsModalOpen(true);
        const audioElement = audioRef.current;
        if (audioElement) {
            audioElement.addEventListener('ended', handleEnded);
            return () => {
                audioElement.removeEventListener('ended', handleEnded);
            };
        }
    }, []);
    const handleCloseAndPlay = () => {
        setIsModalOpen(false);
        handlePlay();
    }
    return (
        <div>
            <audio ref={audioRef}>
                <source
                    src={assessment?.audioFileUrl}
                    type="audio/mp3"
                />
                Your browser does not support the audio element.
            </audio>
            <Modal title="Warning" open={isModalOpen} footer={null} className="custom-modal">
                <div className="modal-content">
                    Lorem ipsum dolor sit, amet consectetur adipisicing elit. Veniam alias officia magnam accusamus natus est temporibus iste. Dignissimos maiores distinctio eum officiis vel totam earum laborum ad. Molestias, obcaecati laudantium!
                </div>
                <Button onClick={handleCloseAndPlay} className="custom-button">Understood!</Button>
            </Modal>
            {isVisible && (
                <div>Shotga qoyin</div>
            )}
            {isPlaying && (<div className="loader">
                <div></div>
                <div></div>
                <div></div>
                <div></div>
                <div></div>
                <div></div>
                <div></div>
                <div></div>
                <div></div>
            </div>)}

        </div>
    );
}
