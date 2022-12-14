import {
  Table,
  Input,
  Button,
  Space,
  Select,
  Drawer,
  Form,
  Checkbox,
  notification,
  Tooltip,
  Card,
} from "antd"
import {
  CheckOutlined,
  RedoOutlined,
  CloseCircleOutlined,
} from "@ant-design/icons"
import { useEffect, useState } from "react"
import ImageUploading from "react-images-uploading"
import moment from "moment"

import makeRequest from "../../utils/makeRequest"
import { RECORD_MODE } from "../../const/mode"
import { SEX, SEX_ARR } from "../../const/member"
import { addListKey } from "../../utils/addListKey"
import omitNil from "../../utils/omit"
import rfidHandle from "../../handle/rfid"
import { requestUrl } from "../../resource/requestUrl"
import SearchText from "../seachItem/SearchText"
import CustomSkeleton from "../skeleton/CustomSkeleton"
import { host } from "../../resource/requestUrl"

import "./Member.css"

const { Option } = Select

const MemberManagement = ({ config }) => {
  const [members, setMembers] = useState([])
  const [userUnits, setUserUnits] = useState([])
  const [rfids, setRfids] = useState([])
  const [displayRfids, setDisplayRfids] = useState([])
  const [loading, setLoading] = useState(true)
  const [pagination, setPagination] = useState({
    current: 1,
    pageSize: 10,
    total: 0,
    showTotal: (total, range) => `${range[0]}-${range[1]} of ${total}`,
  })

  const [filter, setFilter] = useState({
    employeeCode: "",
    name: "",
    sex: "",
    phone: "",
    email: "",
    status: null,
    unitId: null,
  })
  const [lastFilter, setLastFilter] = useState({
    employeeCode: "",
    name: "",
    sex: "",
    phone: "",
    email: "",
    status: null,
    unitId: null,
  })

  const [selectedRowArr, setSelectedRowArr] = useState([])
  const [visible, setVisible] = useState(false)
  const [mode, setMode] = useState(RECORD_MODE.CREATE)
  const [selectedRecord, setSelectedRecord] = useState({})
  const [images, setImages] = useState([])

  const handleSearch = async (params, pagination = {}) => {
    let filter = omitNil(params)
    setLoading(true)
    makeRequest({
      method: "GET",
      url: config.url.readUrl(),
      params: {
        ...filter,
        page: pagination.current,
        record: pagination.pageSize,
      },
    }).then((searchRs) => {
      setMembers(addListKey(searchRs.data))
      setPagination({
        ...pagination,
        total: searchRs.totalRecords,
      })
      setLoading(false)
    })
  }

  const handlePaginationChange = (page, pageSize) => {
    let newPagination = { ...pagination, current: page, pageSize }
    handleSearch(filter, newPagination)
  }

  const columns = [
    {
      title: "M?? c??n b???",
      dataIndex: "employeeCode",
      width: 120,
    },
    {
      title: "H??? t??n",
      dataIndex: "name",
      width: 170,
    },
    {
      title: "Gi???i t??nh",
      dataIndex: "sex",
      render: (text, record) => {
        return <>{SEX[text]}</>
      },
      width: 100,
    },
    {
      title: "??i???n tho???i",
      dataIndex: "phone",
      width: 120,
    },
    {
      title: "Email",
      dataIndex: "email",
      width: 200,
    },
    {
      title: "Th??? RFID",
      dataIndex: "rfidNumber",
      render: (text, record) => {
        return <>{record.rfid ? record.rfid.cardNumber : ""}</>
      },
      width: 200,
      ellipsis: true,
    },
    {
      title: "????n v???",
      render: (text, record) => {
        return <>{record.unit.name}</>
      },
      width: 150,
    },
    {
      title: "T??nh tr???ng",
      render: (text, record) => {
        return <>{record.status ? "??ang l??m" : "???? ngh??? vi???c"}</>
      },
    },
    {
      title: "H??nh ?????ng",
      render: (text, record) => {
        return (
          <Space>
            <Button type="default" danger size="small">
              G??? RFID
            </Button>
            <CloseCircleOutlined style={{ color: "red" }} />
          </Space>
        )
      },
      align: "center",
      width: 150,
    },
  ]

  // Function handle selectedRow
  const onRowSelected = (record, selected, selectedRows) => {
    setVisible(true)
    let selectedRecord = selectedRows[0]
    setSelectedRecord({ ...selectedRecord })
    form.resetFields()
    form.setFieldsValue({ ...selectedRecord })
    setMode(RECORD_MODE.UPDATE)

    setDisplayRfids(
      rfidHandle.setListRfid(rfids, selectedRows[0].rfid, RECORD_MODE.UPDATE)
    )
  }

  const onSelectedChange = (selectedRowKeys, selectedRows) => {
    setSelectedRowArr(selectedRowKeys)
  }

  // Function handle drawer
  const onClose = () => {
    setSelectedRowArr([])
    setSelectedRecord({})
    setImages([])
    setVisible(false)
  }

  useEffect(() => {
    const getData = async () => {
      let [memberRs, rfidRs, userUnitRs] = await Promise.all([
        makeRequest({
          method: "GET",
          url: config.url.readUrl(),
        }),
        makeRequest({
          method: "GET",
          url: requestUrl.rfid.readUrl(),
          params: config.rfidConfig,
        }),
        makeRequest({
          method: "GET",
          url: requestUrl.userUnit.readUrl(),
        }),
      ])
      setMembers(addListKey(memberRs.data))
      setRfids(addListKey(rfidRs.data))
      setUserUnits(addListKey(userUnitRs.data))
      setDisplayRfids(
        rfidHandle.setListRfid(rfidRs.data, selectedRecord.rfid, mode)
      )
      setPagination({ ...pagination, total: memberRs.totalRecords })
      setLoading(false)
    }

    getData()
    return () => {
      setPagination({
        current: 1,
        pageSize: 10,
        total: 0,
      })
    }
  }, [])

  const onImgChange = (imageList, addUpdateIndex) => {
    // data for submit
    console.log(imageList, addUpdateIndex)
    setImages(imageList)
  }

  const [form] = Form.useForm()

  const onFinish = (member) => {
    setLoading(true)
    let data = { ...member }
    if (images.length) {
      let img = images[0]
      let imgName = img.file.name
      let currentTimeStr = moment().format("YYYYMMDD_HHmmss")
      let [fileName, etc] = imgName.split(".")
      imgName = `${fileName}_${currentTimeStr}.${etc}`
      data.avatar = imgName
      makeRequest({
        method: "POST",
        url: requestUrl.uploadAvata.createUrl(),
        data: {
          image: img.data_url,
          imgName,
        },
      })
    }
    let method = mode === RECORD_MODE.CREATE ? "POST" : "PUT"
    let msg =
      mode === RECORD_MODE.CREATE
        ? "Th??m m???i th??nh c??ng"
        : "C???p nh???t th??nh c??ng"
    let url =
      mode === RECORD_MODE.CREATE
        ? config.url.createUrl()
        : config.url.updateUrl({ id: member.id })
    makeRequest({
      method,
      url,
      data,
    }).then((rs) => {
      notification.open({
        message: "Th??ng b??o",
        icon: <CheckOutlined style={{ color: "#2fd351" }} />,
        description: msg,
      })

      handleSearch(filter, pagination)
      handleReloadRfid(mode)
      setSelectedRowArr([])
      setSelectedRecord({})
      setVisible(false)
      setRfids(rfidHandle.rmDistributedRfid(rfids, { id: member.rfidid }))
      setLoading(false)
    })
  }

  const openCreateForm = () => {
    setVisible(true)
    setMode(RECORD_MODE.CREATE)
    setSelectedRecord({})
    form.resetFields()
    form.setFieldsValue({})
    setDisplayRfids(
      rfidHandle.setListRfid(rfids, selectedRecord.rfid, RECORD_MODE.CREATE)
    )
  }

  const handleReloadRfid = () => {
    makeRequest({
      method: "GET",
      url: requestUrl.rfid.readUrl(),
      params: config.rfidConfig,
    })
      .then((rs) => {
        let rfids = rs.data
        setRfids(rfids)
        setDisplayRfids(
          rfidHandle.setListRfid(rfids, selectedRecord.rfid, mode)
        )
      })
      .catch((err) => {
        console.log(err)
      })
  }

  return (
    <>
      <Card
        title={config.tableTitle}
        extra={
          <Button type="primary" onClick={openCreateForm}>
            Th??m c??n b???
          </Button>
        }
        style={{ height: "100%" }}
      >
        <Space style={{ marginBottom: 10 }}>
          <SearchText
            dataIndex="employeeCode"
            filter={filter}
            pagination={pagination}
            handleSearch={handleSearch}
            placeholder={"M?? c??n b???"}
            setFilter={setFilter}
            setLastFilter={setLastFilter}
            lastFilter={lastFilter}
          ></SearchText>
          <SearchText
            dataIndex="email"
            filter={filter}
            pagination={pagination}
            handleSearch={handleSearch}
            placeholder={"Email"}
            setFilter={setFilter}
            setLastFilter={setLastFilter}
            lastFilter={lastFilter}
          ></SearchText>
          <SearchText
            dataIndex="phone"
            filter={filter}
            pagination={pagination}
            handleSearch={handleSearch}
            placeholder={"S??? ??i???n tho???i"}
            setFilter={setFilter}
            setLastFilter={setLastFilter}
            lastFilter={lastFilter}
          ></SearchText>
        </Space>
        <Table
          dataSource={members}
          columns={columns}
          pagination={{
            position: ["bottomRight"],
            ...pagination,
            onChange: handlePaginationChange,
          }}
          size="small"
          rowSelection={{
            selectedRowKeys: selectedRowArr,
            columnTitle: <div>S???a</div>,
            columnWidth: 50,
            type: "checkbox",
            onSelect: (record, selected, selectedRows) =>
              onRowSelected(record, selected, selectedRows),
            onChange: (selectedRowKeys, selectedRows) =>
              onSelectedChange(selectedRowKeys, selectedRows),
          }}
        />
      </Card>
      {/* <div style={{ display: "flex", justifyContent: "space-between" }}>
        <h2>{config.tableTitle}</h2>
        <Button type="primary" onClick={openCreateForm}>
          Th??m c??n b???
        </Button>
      </div> */}

      <Drawer
        title={
          mode === RECORD_MODE.CREATE ? "Th??m c??n b???" : "C???p nh???t th??ng tin"
        }
        placement="right"
        onClose={onClose}
        visible={visible}
      >
        <ImageUploading
          value={images}
          onChange={onImgChange}
          dataURLKey="data_url"
          acceptType={["jpg", "jpeg", "png"]}
        >
          {({ imageList, onImageUpload, dragProps }) => (
            // write your building UI
            <div
              className="upload__image-wrapper"
              style={{ display: "flex", flexDirection: "column" }}
            >
              {imageList.length ? (
                imageList.map((image, index) => (
                  <img
                    src={image.data_url}
                    alt=""
                    width="150"
                    height="200"
                    style={{ margin: "auto" }}
                    className="avata"
                  />
                ))
              ) : selectedRecord.avatar ? (
                <img
                  src={
                    selectedRecord.avatar.includes("robohash")
                      ? selectedRecord.avatar
                      : `${host}/avata/${selectedRecord.avatar}`
                  }
                  alt=""
                  width="150"
                  height="200"
                  style={{ margin: "auto" }}
                  className="avata"
                />
              ) : (
                <img
                  src={`${host}/images/DEFAULT_AVATA.jpg`}
                  alt=""
                  width="150"
                  height="200"
                  style={{ margin: "auto" }}
                  className="avata"
                />
              )}
              <button
                style={{
                  margin: "10px auto",
                }}
                onClick={onImageUpload}
                {...dragProps}
              >
                Ch???n ???nh ?????i di???n
              </button>
            </div>
          )}
        </ImageUploading>
        <Form
          name="memberForm"
          labelCol={{ span: 8 }}
          wrapperCol={{ span: 16 }}
          initialValues={{ ...selectedRecord }}
          form={form}
          onFinish={onFinish}
          labelAlign="left"
        >
          <Form.Item label="id" name="id" noStyle>
            <Input type="hidden" />
          </Form.Item>
          <Form.Item
            label="M?? c??n b???"
            name="employeeCode"
            rules={[
              {
                required: true,
                message: "Tr?????ng n??y kh??ng ???????c thi???u",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item name="avatar" noStyle>
            <Input type="hidden" />
          </Form.Item>
          <Form.Item
            label="H??? t??n"
            name="name"
            rules={[
              {
                required: true,
                message: "Tr?????ng n??y kh??ng ???????c thi???u",
              },
            ]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            name="sex"
            label="Gi???i t??nh"
            rules={[{ required: true, message: "Tr?????ng n??y kh??ng ???????c thi???u" }]}
          >
            <Select>
              {SEX_ARR.map((item, index) => (
                <Option value={item.value} key={index}>
                  {item.label}
                </Option>
              ))}
            </Select>
          </Form.Item>

          <Form.Item label="S??T" name="phone">
            <Input />
          </Form.Item>

          <Form.Item label="Email" name="email">
            <Input />
          </Form.Item>

          <Form.Item
            name="unitId"
            label="????n v???"
            rules={[{ required: true, message: "B???n ph???i ch???n ????n v???" }]}
          >
            <Select>
              {userUnits.map((item, index) => (
                <Option value={item.id} key={index}>
                  {item.name}
                </Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item
            name="rfidid"
            label={
              <>
                Th??? RFID [
                <Tooltip title="Reload RFID">
                  <RedoOutlined
                    onClick={() => handleReloadRfid(mode)}
                    style={{ color: "#1890ff" }}
                  />
                </Tooltip>
                ]
              </>
            }
          >
            <Select>
              {displayRfids.map((item, index) => (
                <Option value={item.id} key={index}>
                  {item.cardNumber}
                </Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item name="status" label="C??n l??m vi???c" valuePropName="checked">
            <Checkbox />
          </Form.Item>

          <Form.Item
            wrapperCol={{
              offset: 0,
              span: 24,
            }}
          >
            <Button type="primary" htmlType="submit">
              L??u
            </Button>
          </Form.Item>
        </Form>
      </Drawer>
      <CustomSkeleton loading={loading} />
    </>
  )
}

export default MemberManagement
