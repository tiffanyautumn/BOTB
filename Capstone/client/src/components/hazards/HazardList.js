import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Table } from "reactstrap"
import { getAllHazards } from "../../modules/hazardManager"
import { Hazards } from "./Hazards"

export const HazardList = ({ isAdmin, isApproved, searchTermState }) => {
    const [hazards, setHazards] = useState([])
    const navigate = useNavigate()

    const getHazards = () => {
        getAllHazards().then(h => setHazards(h))
    }

    useEffect(() => {
        getHazards();
    }, []);


    return (
        <div className="container">
            <p className="Name panel-item">
                <span className="title">Hazards {isAdmin ? <button onClick={(() => navigate('/hazard/create'))} className="btn"><i className="fa-solid fa-plus"></i></button> : ""}</span>
            </p>
            <div className="row ">

                {hazards.map((hazard) => (
                    <Hazards hazard={hazard} key={hazard.id} isAdmin={isAdmin} />
                ))}


            </div>

        </div>
    )
}