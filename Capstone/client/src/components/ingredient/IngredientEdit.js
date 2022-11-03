import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { editIngredient, getIngredientById } from "../../modules/ingredientManager"

export const IngredientEdit = () => {
    const navigate = useNavigate()
    const { ingredientId } = useParams()
    const [ingredient, updateIngredient] = useState({
        id: 0,
        name: "",
        imageUrl: ""
    })

    const getIngredient = () => {
        getIngredientById(ingredientId).then(i => updateIngredient(i))
    }

    useEffect(
        () => {
            getIngredient()
        }, []
    )


    const handleSaveButtonClick = (event) => {
        event.preventDefault()


        const ingredientToSend = {
            id: ingredient.id,
            name: ingredient.name,
            imageUrl: ingredient.imageUrl
        }

        return editIngredient(ingredientId, ingredientToSend)
            .then(() => {
                navigate(`/ingredient/${ingredient.id}`)
            })
    }
    return (<>
        <Card>
            <CardBody>

                <Form className="ingredientForm">
                    <FormGroup>
                        <Label for="name">Ingredient</Label>
                        <Input
                            id="name"
                            name="name"
                            type="text"
                            value={ingredient.name}
                            onChange={
                                (evt) => {
                                    const copy = { ...ingredient }
                                    copy.name = evt.target.value
                                    updateIngredient(copy)
                                }
                            } />
                    </FormGroup>
                    <FormGroup>
                        <Label for="imageUrl">Image Link</Label>
                        <Input
                            id="imageUrl"
                            name="imageUrl"
                            type="text"
                            value={ingredient.imageUrl}
                            onChange={
                                (evt) => {
                                    const copy = { ...ingredient }
                                    copy.imageUrl = evt.target.value
                                    updateIngredient(copy)
                                }
                            } />
                    </FormGroup>



                    <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                        className="btn btn-primary">
                        Save
                    </Button>
                </Form>
                <Button onClick={() => navigate(`/ingredient/${ingredient.id}`)}>Cancel</Button>
            </CardBody>
        </Card>
    </>)
}