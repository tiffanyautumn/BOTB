import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { getAllIngredients } from "../../modules/ingredientManager"
import { addProductIngredient } from "../../modules/productIngredientManager"

export const ProductIngredientForm = ({ product, setFormActive, getProduct, getPIs }) => {
    const navigate = useNavigate()
    const [ingredients, setIngredients] = useState([])
    const [productIngredient, updateProductIngredient] = useState({
        ingredientId: 0,
        activeIngredient: false,
        order: 0
    })

    const getIngredients = () => {
        getAllIngredients().then(p => setIngredients(p))
    }

    useEffect(() => {
        getIngredients();
    }, []);



    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        if (productIngredient.use === "") {
            productIngredient.use = null
        }


        const productIngredientToSend = {
            ingredientId: parseInt(productIngredient.ingredientId),
            productId: product.id,
            activeIngredient: productIngredient.activeIngredient,
            order: parseInt(productIngredient.order)
        }

        return addProductIngredient(productIngredientToSend)
            .then(() => {
                setFormActive(false)
                getProduct()
                getPIs()
            })
    }
    return (<>

        <div className="productingredientform">

            <span className="title">Add an Ingredient</span>
            <FormGroup>
                <Label for="ingredientSelect">Ingredient</Label>
                <Input
                    id="ingredientSelect"
                    name="ingredientId"
                    type="select"
                    onChange={
                        (evt) => {
                            const copy = { ...productIngredient }
                            copy.ingredientId = evt.target.value
                            updateProductIngredient(copy)
                        }
                    }>
                    <option>choose an ingredient</option>
                    {ingredients.map((i) => (
                        <option value={i.id} key={i.id}>{i.name}</option>
                    ))}
                </Input>
            </FormGroup>
            <Form className="ingredientForm">
                <FormGroup>
                    <Label for="activeIngredient">Active</Label>
                    <Input
                        id="activeIngredient"
                        name="activeIngredient"
                        type="checkbox"
                        value={productIngredient.activeIngredient}
                        onChange={
                            (evt) => {
                                const copy = { ...productIngredient }
                                copy.activeIngredient = evt.target.checked
                                updateProductIngredient(copy)
                            }
                        } />
                </FormGroup>
                <FormGroup>
                    <Label for="order">Order</Label>
                    <Input
                        id="order"
                        name="order"
                        type="number"
                        value={productIngredient.order}
                        onChange={
                            (evt) => {
                                const copy = { ...productIngredient }
                                copy.order = evt.target.value
                                updateProductIngredient(copy)
                            }
                        } />
                </FormGroup>

                <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                    className="btn btn-primary">
                    Save
                </Button>
            </Form>
            <Button onClick={() => setFormActive(false)}>Cancel</Button>

        </div>
    </>)
}